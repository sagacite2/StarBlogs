using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StarBlogs.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(15)]
        [MinLength(2)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(25)]
        [MinLength(5)]
        public string Password { get; set; }

        public string EmailOrPhoneNumber { get; set; }
    }
}