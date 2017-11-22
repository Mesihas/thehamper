using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamperWeb.Models;

namespace HamperWeb.Models
{
    public class Hamper {
        public int HamperId { get; set; }
        public int CategoryId { get; set; }
        public string HamperName { get; set; }
        public decimal HamperPrice { get; set; }
        public string Description { get; set; }
        public bool Discontinued { get; set; }  
        public string ImageUrl { get; set; }
    }
}
