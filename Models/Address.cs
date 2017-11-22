using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HamperWeb.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public int ProfileId { get; set; }
        public string UserId { get; set; }
        public string AddressUser { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool Default { get; set; }
        public bool Deleted { get; set; }
    }
}
