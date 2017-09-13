using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ContactsHRC.Context;
using ContactsHRC.Models;

namespace ContactsHRC.Controllers
{
    public class ContactsController : ApiController
    {
        private ContactsContext _context = new ContactsContext();

        // GET: api/Contacts
        public IQueryable<Contact> GetContacts()
        {

            return _context.Contacts;
        }

        // GET: api/Contacts?filter="filt"
        public IQueryable<Contact> GetFilteredContacts(String filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return GetContacts();
            }

            filter = filter.ToUpper();

            return _context.Contacts.Where(c =>
                c.FirstName.ToUpper().Contains(filter) || c.LastName.ToUpper().Contains(filter) ||
                c.Tags.Any(t => t.TagName.ToUpper().Contains(filter)));

        }

        // GET: api/Contacts/5
        [ResponseType(typeof(Contact))]
        public IHttpActionResult GetContact(int id)
        {
            Contact contact = _context.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/Contacts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContact(int id, Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.ContactId)
            {
                return BadRequest();
            }

            UpdatePhoneNumbers(contact);

            UpdateEmailAddresses(contact);

            UpdateTags(contact);


            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }



        // POST: api/Contacts
        [ResponseType(typeof(Contact))]
        public IHttpActionResult PostContact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            _context.Contacts.Add(contact);
            _context.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contact.ContactId }, contact);
        }

        // DELETE: api/Contacts/5
        [ResponseType(typeof(Contact))]
        public IHttpActionResult DeleteContact(int id)
        {

            var contact = _context.Contacts.FirstOrDefault(c => c.ContactId == id);

            _context.EmailAddresses.RemoveRange(contact.EmailAddresses);
            _context.PhoneNumbers.RemoveRange(contact.PhoneNumbers);
            _context.Tags.RemoveRange(contact.Tags);

            _context.Contacts.Remove(contact);
            _context.SaveChanges();

            return Ok(contact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Count(e => e.ContactId == id) > 0;
        }


        private void UpdatePhoneNumbers(Contact contact)
        {
            var originalContact = _context.Contacts.Where(c => c.ContactId == contact.ContactId).Include(c => c.PhoneNumbers).SingleOrDefault();

            var contactEntry = _context.Entry(originalContact);
            contactEntry.CurrentValues.SetValues(contact);

            foreach (var number in contact.PhoneNumbers)
            {
                var originalNumber = originalContact.PhoneNumbers.SingleOrDefault(n => n.PhoneNumberId == number.PhoneNumberId && n.PhoneNumberId != 0);


                if (originalNumber != null)
                {
                    var numberEntry = _context.Entry(originalNumber);
                    numberEntry.CurrentValues.SetValues(number);
                }
                else
                {
                    number.PhoneNumberId = 0;
                    originalContact.PhoneNumbers.Add(number);
                }
            }

            foreach (var originalNumber in originalContact.PhoneNumbers.Where(n => n.PhoneNumberId != 0).ToList())
            {
                if (contact.PhoneNumbers.All(n => n.PhoneNumberId != originalNumber.PhoneNumberId))
                {
                    _context.PhoneNumbers.Remove(originalNumber);
                }
            }
        }

        private void UpdateEmailAddresses(Contact contact)
        {
            var originalContact = _context.Contacts.Where(c => c.ContactId == contact.ContactId).Include(c => c.EmailAddresses).SingleOrDefault();

            var contactEntry = _context.Entry(originalContact);
            contactEntry.CurrentValues.SetValues(contact);

            foreach (var emailAddress in contact.EmailAddresses)
            {
                var originalEmail = originalContact.EmailAddresses.SingleOrDefault(n => n.EmailAddressId == emailAddress.EmailAddressId && n.EmailAddressId != 0);


                if (originalEmail != null)
                {
                    var emailEntry = _context.Entry(originalEmail);
                    emailEntry.CurrentValues.SetValues(emailAddress);
                }
                else
                {
                    emailAddress.EmailAddressId = 0;
                    originalContact.EmailAddresses.Add(emailAddress);
                }
            }

            foreach (var originalEmail in originalContact.EmailAddresses.Where(n => n.EmailAddressId != 0).ToList())
            {
                if (contact.EmailAddresses.All(n => n.EmailAddressId != originalEmail.EmailAddressId))
                {
                    _context.EmailAddresses.Remove(originalEmail);
                }
            }
        }

        private void UpdateTags(Contact contact)
        {
            var originalContact = _context.Contacts.Where(c => c.ContactId == contact.ContactId).Include(c => c.Tags).SingleOrDefault();

            var contactEntry = _context.Entry(originalContact);
            contactEntry.CurrentValues.SetValues(contact);

            foreach (var tag in contact.Tags)
            {
                var originalTag = originalContact.Tags.SingleOrDefault(n => n.TagId == tag.TagId && n.TagId != 0);


                if (originalTag != null)
                {
                    var tagEntry = _context.Entry(originalTag);
                    tagEntry.CurrentValues.SetValues(tag);
                }
                else
                {
                    // check if that tagname already exists in db,
                    if (_context.Tags.SingleOrDefault(t => t.TagName.Equals(tag.TagName)) != null)
                    {
                        // if it does, then just update tags list of contacts
                        _context.Tags.SingleOrDefault(t => t.TagName.Equals(tag.TagName)).Contacts.Add(originalContact);
                    }
                    else
                    {
                        //otherwise, add tag to db
                        tag.TagId = 0;
                        originalContact.Tags.Add(tag);
                    }
                }
            }

            foreach (var originalTag in originalContact.Tags.Where(n => n.TagId != 0).ToList())
            {
                if (contact.Tags.All(n => n.TagId != originalTag.TagId))
                {
                    // check if there is multiple contacts assigned to this tag
                    if (_context.Tags.Single(t => t.TagId == originalTag.TagId).Contacts.Count > 1)
                    {
                        originalContact.Tags.Remove(originalTag);
                    }
                    else
                    {
                        _context.Tags.Remove(originalTag);
                    }
                }
            }
        }
    }
}