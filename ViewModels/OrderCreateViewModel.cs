using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamperWeb.Models;
namespace HamperWeb.ViewModels
{
    public class OrderCreateViewModel
    {    
        public IEnumerable<Category> Categories { get; set; }
        public int CategoryId { get; set; }
    }
}
