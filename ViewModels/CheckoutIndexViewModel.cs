using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamperWeb.Models;
namespace HamperWeb.ViewModels
{
    public class CheckoutIndexViewModel
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public int OrderState { get; set; }
        public int totalItems { get; set; }
        public string SumItems { get; set; }
        public IEnumerable<Address> AddressList { get; set; }
        public int TotalAddress { get; set; }
        public int DefaultAddress { get; set; }
    }
}
