using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamperWeb.Models;

namespace HamperWeb.ViewModels
{
    public class HamperUpdateViewModel
    {
        public int HamperId { get; set; }
        public int CategoryId { get; set; }
        public string HamperName { get; set; }
        public decimal HamperPrice { get; set; }
        public string Description { get; set; }
        public bool Discontinued { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}