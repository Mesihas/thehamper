using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HamperWeb.Models
{
    public class OrderMongo
    {
        public Guid OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public int AddressId { get; set; }
        public int OrderStateId { get; set; }
    }
}
