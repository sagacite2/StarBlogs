using Abp.AutoMapper;
using StarBlogs.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Translates.Dtos
{
    [AutoMapFrom(typeof(Translation))]
    public class TranslationWithPostDto : TranslationDto
    {
        public OriginalPost OriginalPost { get; set; }
    }
}
