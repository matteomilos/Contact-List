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
        private ContactsContext db = new ContactsContext();

        // GET: api/Contacts
        public IQueryable<Contact> GetContacts()
        {
            return db.Contacts;
        }

        // GET: api/Contacts/5
        [ResponseType(typeof(Contact))]
        public IHttpActionResult GetContact(int id)
        {
            Contact contact = db.Contacts.Find(id);
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
            //db.Entry(contact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
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

        private void UpdatePhoneNumbers(Contact contact)
        {
            var originalContact = db.Contacts.Where(c => c.ContactId == contact.ContactId).Include(c => c.PhoneNumbers).SingleOrDefault();

            var contactEntry = db.Entry(originalContact);
            contactEntry.CurrentValues.SetValues(contact);

            foreach (var number in contact.PhoneNumbers)
            {
                var originalNumber = originalContact.PhoneNumbers.SingleOrDefault(n => n.PhoneNumberId == number.PhoneNumberId && n.PhoneNumberId != 0);


                if (originalNumber != null)
                {
                    var numberEntry = db.Entry(originalNumber);
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
                    db.PhoneNumbers.Remove(originalNumber);
                }
            }
        }

        private void UpdateEmailAddresses(Contact contact)
        {
            var originalContact = db.Contacts.Where(c => c.ContactId == contact.ContactId).Include(c => c.EmailAddresses).SingleOrDefault();

            var contactEntry = db.Entry(originalContact);
            contactEntry.CurrentValues.SetValues(contact);

            foreach (var number in contact.EmailAddresses)
            {
                var originalNumber = originalContact.EmailAddresses.SingleOrDefault(n => n.EmailAddressId == number.EmailAddressId && n.EmailAddressId != 0);


                if (originalNumber != null)
                {
                    var numberEntry = db.Entry(originalNumber);
                    numberEntry.CurrentValues.SetValues(number);
                }
                else
                {
                    number.EmailAddressId = 0;
                    originalContact.EmailAddresses.Add(number);
                }
            }

            foreach (var originalNumber in originalContact.EmailAddresses.Where(n => n.EmailAddressId != 0).ToList())
            {
                if (contact.EmailAddresses.All(n => n.EmailAddressId != originalNumber.EmailAddressId))
                {
                    db.EmailAddresses.Remove(originalNumber);
                }
            }
        }

        private void UpdateTags(Contact contact)
        {
            var originalContact = db.Contacts.Where(c => c.ContactId == contact.ContactId).Include(c => c.Tags).SingleOrDefault();

            var contactEntry = db.Entry(originalContact);
            contactEntry.CurrentValues.SetValues(contact);

            foreach (var number in contact.Tags)
            {
                var originalNumber = originalContact.Tags.SingleOrDefault(n => n.TagId == number.TagId && n.TagId != 0);


                if (originalNumber != null)
                {
                    var numberEntry = db.Entry(originalNumber);
                    numberEntry.CurrentValues.SetValues(number);
                }
                else
                {
                    number.TagId = 0;
                    originalContact.Tags.Add(number);
                }
            }

            foreach (var originalNumber in originalContact.Tags.Where(n => n.TagId != 0).ToList())
            {
                if (contact.Tags.All(n => n.TagId != originalNumber.TagId))
                {
                    db.Tags.Remove(originalNumber);
                }
            }
        }

        // POST: api/Contacts
        [ResponseType(typeof(Contact))]
        public IHttpActionResult PostContact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.Contacts.Add(contact);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contact.ContactId }, contact);
        }

        // DELETE: api/Contacts/5
        [ResponseType(typeof(Contact))]
        public IHttpActionResult DeleteContact(int id)
        {

            var contact = db.Contacts.FirstOrDefault(c => c.ContactId == id);

            db.EmailAddresses.RemoveRange(contact.EmailAddresses);
            db.PhoneNumbers.RemoveRange(contact.PhoneNumbers);
            db.Tags.RemoveRange(contact.Tags);

            db.Contacts.Remove(contact);
            db.SaveChanges();

            return Ok(contact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactExists(int id)
        {
            return db.Contacts.Count(e => e.ContactId == id) > 0;
        }
    }
}