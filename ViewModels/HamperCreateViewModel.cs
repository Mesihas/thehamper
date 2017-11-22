using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamperWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace HamperWeb.ViewModels
{
    public class HamperCreateViewModel
    {
        public int CategoryId { get; set; }
        public string HamperName { get; set; }
        public decimal HamperPrice { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}