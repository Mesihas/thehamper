using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HamperWeb.ViewModels
{
    public class HamperDetailViewModel
    {
        public int HamperId { get; set; }
        public string HamperName { get; set; }
        public decimal HamperPrice { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public int totalItems { get; set; }
        public string SumItems { get; set; }
    }
}


