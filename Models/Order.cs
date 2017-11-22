using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamperWeb.Models;

namespace HamperWeb.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }       
        public int AddressId { get; set; }   
        public int OrderStateId { get; set; }                 
    }
}
