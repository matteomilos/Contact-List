namespace ContactsHRC.Migrations
{
    using ContactsHRC.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ContactsHRC.Context.ContactsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ContactsHRC.Context.ContactsContext context)
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

            List<EmailAddress> emails1 = new List<EmailAddress>()
                    {
                        new EmailAddress
                        {
                            EmailAddressValue = "email1@gmail.com"
                        },
                        new EmailAddress
                        {
                            EmailAddressValue = "email2@yahoo.com"
                        }
                    };

            List<EmailAddress> emails2 = new List<EmailAddress>()
                    {
                        new EmailAddress
                        {
                            EmailAddressValue = "mail4@net.hr"
                        },
                        new EmailAddress
                        {
                            EmailAddressValue = "1mail1@outlook.com"
                        }
                    };

            context.Contacts.AddOrUpdate(c => c.ContactId,
                new Contact
                {
                    FirstName = "Matteo",
                    LastName = "Miloš",
                    Address = "Zagrebaèka 23, Zagreb",

                    EmailAddresses =emails1,

                    PhoneNumbers = new List<PhoneNumber>()
                    {
                        new PhoneNumber
                        {
                            PhoneNumberValue = "098251236"
                        },
                        new PhoneNumber
                        {
                            PhoneNumberValue = "013578965"
                        }
                    }
                },

                new Contact
                {
                    FirstName = "Ivan",
                    LastName = "Horvat",
                                        Address = "Splitska 23, Split",

                    EmailAddresses = emails2,

                    PhoneNumbers = new List<PhoneNumber>()
                    {
                        new PhoneNumber
                        {
                            PhoneNumberValue = "098325687"
                        },
                        new PhoneNumber
                        {
                            PhoneNumberValue = "021258412"
                        }
                    }
                }
                );
        }


    }
}
