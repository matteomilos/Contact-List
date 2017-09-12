using System.Collections.Generic;
using System.Data.Entity.Migrations;
using ContactsHRC.Context;
using ContactsHRC.Models;

namespace ContactsHRC.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ContactsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ContactsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            List<EmailAddress> emails1 = new List<EmailAddress>
            {
                new EmailAddress{EmailAddressValue = "email1@gmail.com"},
                new EmailAddress{EmailAddressValue = "email2@yahoo.com"}
            };

            List<EmailAddress> emails2 = new List<EmailAddress>
            {
                new EmailAddress{EmailAddressValue = "mail4@net.hr"},
                new EmailAddress{EmailAddressValue = "1mail1@outlook.com"}
            };

            List<PhoneNumber> numbers1 = new List<PhoneNumber>
            {
                new PhoneNumber{PhoneNumberValue = "098251236"},
                new PhoneNumber{PhoneNumberValue = "013578965"}
            };

            List<PhoneNumber> numbers2 = new List<PhoneNumber>
            {
                new PhoneNumber{PhoneNumberValue = "098325687"},
                new PhoneNumber{PhoneNumberValue = "021258412"}
            };

            List<Tag> tags1 = new List<Tag>
            {
                new Tag{TagName = "mislav"},
                new Tag{TagName = "mlad"},
                new Tag{TagName = "pametan"}
            };

            List<Tag> tags2 = new List<Tag>
            {
                new Tag{TagName = "ivan"},
                new Tag{TagName = "star"},
                new Tag{TagName = "zanimljiv"}
            };

            context.Contacts.AddOrUpdate(c => c.ContactId,
                new Contact
                {
                    FirstName = "Mislav",
                    LastName = "Mariæ",
                    Address = "Zagrebaèka 23, Zagreb",
                    EmailAddresses = emails1,
                    PhoneNumbers = numbers1,
                    Tags = tags1
                },

                new Contact
                {
                    FirstName = "Ivan",
                    LastName = "Horvat",
                    Address = "Splitska 23, Split",
                    EmailAddresses = emails2,
                    PhoneNumbers = numbers2,
                    Tags = tags2
                }
                );
        }


    }
}
