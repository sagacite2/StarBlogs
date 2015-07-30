using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Helpers
{
    public enum Gender
    {
        [Description("男")]
        Male = 0,
        [Description("女")]
        Female = 1,
        [Description("")]
        Undefined = 2
    }
}
