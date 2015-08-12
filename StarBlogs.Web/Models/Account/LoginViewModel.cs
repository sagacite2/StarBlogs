using System.ComponentModel.DataAnnotations;

namespace StarBlogs.Web.Models.Account
{
    public class LoginViewModel
    {
        public string TenancyName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}