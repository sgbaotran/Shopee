using System;
using System.ComponentModel.DataAnnotations;

namespace Shopee.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your email address"), EmailAddress]
        public string EmailAddress { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Please enter your password"), Required(ErrorMessage = "Please enter your UserName")]
        public string Password { get; set; }


        public UserModel()
        {
        }
    }
}

