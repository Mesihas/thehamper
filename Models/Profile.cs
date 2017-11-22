using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamperWeb.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HamperWeb.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
