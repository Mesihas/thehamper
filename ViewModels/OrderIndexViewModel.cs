using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamperWeb.Models;

namespace HamperWeb.ViewModels
{
    public class OrderIndexViewModel
    {
        public int Total { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Order> Orders { get; set; }

    }
}
