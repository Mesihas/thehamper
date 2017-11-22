using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HamperWeb.ViewModels
{
    public class AccountRegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

     //   [Required, DataType(DataType.EmailAddress)]
     //   public string Email { get; set; }
    }
}
