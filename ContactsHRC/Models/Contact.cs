using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactsHRC.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public List<EmailAddress> EmailAddresses { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }

        public virtual List<Tag> Tags { get; set; }



    }

    public class EmailAddress
    {
        public int EmailAddressId { get; set; }
        public string EmailAddressValue { get; set; }

    }

    public class PhoneNumber
    {
        public int PhoneNumberId { get; set; }
        public string PhoneNumberValue { get; set; }

    }
}