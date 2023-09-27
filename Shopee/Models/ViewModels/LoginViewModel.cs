using System;
using System.ComponentModel.DataAnnotations;

namespace Shopee.Models.ViewModels
{
    public class LoginViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your username")]
        public string UserName { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Please enter your password"), Required(ErrorMessage = "Please enter your UserName")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
        public LoginViewModel()
        {
        }
    }
}

