﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HamperWeb.Models
{
    public class OrderDetailMongo2
    {
        public int HamperId { get; set; }
        public int Quantity { get; set; }
        public decimal HamperPrice { get; set; }
    }
}
