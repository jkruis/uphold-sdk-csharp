using System;
using System.Collections.Generic;
using System.Text;

namespace UpholdSdk.Models
{
    public class Contact : BaseUpholdEntity
    {
        public string Id { get; set; }
        public List<string> Addresses { get; set; }
        public string Company { get; set; }
        public List<string> Emails { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
    }
}
