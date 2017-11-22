﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using HamperWeb.Models;


namespace HamperWeb.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
