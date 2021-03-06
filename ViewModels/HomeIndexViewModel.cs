﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamperWeb.Models;

namespace HamperWeb.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Hamper> Hampers { get; set; }
        public int CategoryId { get; set; }
        public decimal minPrice { get; set; }
        public decimal maxPrice { get; set; }

    }
}
