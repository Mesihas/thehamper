using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HamperWeb.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int HamperId { get; set; }
        public int Quantity { get; set; }
        public decimal HamperPrice { get; set; }
    }
}
