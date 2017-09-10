using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactsHRC.Models
{
    public class Tag
    {

        public int TagId { get; set; }
        public string TagName { get; set; }

        public virtual List<Contact> Contacts { get; set; }
    }
}