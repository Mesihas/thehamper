using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HamperWeb.Models
{
    public class OrderDetailMongo
    {
        public Guid OrderDetailId { get; set; }
        public string OrderId { get; set; }
        public int HamperId { get; set; }
        public int Quantity { get; set; }
        public decimal HamperPrice { get; set; }
    }
}
