using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Users
{
    public class OptionalUserValidation : UserValidator<User,long>
    {
        public OptionalUserValidation(UserManager applicationUserManager)
            : base(applicationUserManager)
        { }

        public override async Task<IdentityResult> ValidateAsync(User item)
        {
            //父类的验证还是要跑
            IdentityResult result = await base.ValidateAsync(item);

            if (!string.IsNullOrEmpty(item.EmailAddress))
            {
                try
                {
                    var m = new MailAddress(item.EmailAddress);
                }
                catch (FormatException)
                {
                    var errors = result.Errors.ToList();
                    errors.Add("Email地址格式有误！");
                    result = new IdentityResult(errors);
                }
            }
            return result;
        }
    }
}
