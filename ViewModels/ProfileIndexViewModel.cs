using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamperWeb.Models;

namespace HamperWeb.ViewModels
{
    public class ProfileIndexViewModel
    {
        public int ProfileId { get; set; }
        public string UserId { get; set; }       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Total { get; set; }
        public IEnumerable<Address> Address { get; set; }
    }
}
