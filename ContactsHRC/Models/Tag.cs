using System.Collections.Generic;
using Newtonsoft.Json;

namespace ContactsHRC.Models
{
    public class Tag
    {

        public int TagId { get; set; }
        public string TagName { get; set; }
        [JsonIgnore]
        public virtual List<Contact> Contacts { get; set; }
    }
}