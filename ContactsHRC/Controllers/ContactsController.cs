using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
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

            updatePhoneNumbers(contact);

            updateEmailAddresses(contact);

            updateTags(contact);
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
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        private void updatePhoneNumbers(Contact contact)
        {
            var originalContact = db.Contacts.Where(c => c.ContactId == contact.ContactId).Include(c => c.PhoneNumbers).SingleOrDefault();

            var contactEntry = db.Entry(originalContact);
            contactEntry.CurrentValues.SetValues(contact);

            foreach (var number in contact.PhoneNumbers)
            {
                var originalNumber = originalContact.PhoneNumbers.Where(n => n.PhoneNumberId == number.PhoneNumberId && n.PhoneNumberId != 0).SingleOrDefault();


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
                if (!contact.PhoneNumbers.Any(n => n.PhoneNumberId == originalNumber.PhoneNumberId))
                {
                    db.PhoneNumbers.Remove(originalNumber);
                }
            }
        }

        private void updateEmailAddresses(Contact contact)
        {
            var originalContact = db.Contacts.Where(c => c.ContactId == contact.ContactId).Include(c => c.EmailAddresses).SingleOrDefault();

            var contactEntry = db.Entry(originalContact);
            contactEntry.CurrentValues.SetValues(contact);

            foreach (var number in contact.EmailAddresses)
            {
                var originalNumber = originalContact.EmailAddresses.Where(n => n.EmailAddressId == number.EmailAddressId && n.EmailAddressId != 0).SingleOrDefault();


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
                if (!contact.EmailAddresses.Any(n => n.EmailAddressId == originalNumber.EmailAddressId))
                {
                    db.EmailAddresses.Remove(originalNumber);
                }
            }
        }

        private void updateTags(Contact contact)
        {
            var originalContact = db.Contacts.Where(c => c.ContactId == contact.ContactId).Include(c => c.Tags).SingleOrDefault();

            var contactEntry = db.Entry(originalContact);
            contactEntry.CurrentValues.SetValues(contact);

            foreach (var number in contact.Tags)
            {
                var originalNumber = originalContact.Tags.Where(n => n.TagId == number.TagId && n.TagId != 0).SingleOrDefault();


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
                if (!contact.Tags.Any(n => n.TagId == originalNumber.TagId))
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
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

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