using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Blogs
{
    public enum BlogProvider
    {
        [Description("Undefined")]
        UNDEFINED = 0,
        [Description("Facebook")]
        FACEBOOK = 1,
        [Description("Twitter")]
        TWITTER=2,
        [Description("Instagram")]
        INSTAGRAM=3,
        [Description("Linkedin")]
        LINKEDIN=4
    }
}
