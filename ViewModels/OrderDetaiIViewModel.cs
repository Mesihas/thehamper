using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamperWeb.Models;

namespace HamperWeb.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Total { get; set; }
        public int OrderId { set; get; }
        public string CategoryName { get; set; }
        public string State { get; set; }
        public DateTime Date { get; set; }

    }
}
