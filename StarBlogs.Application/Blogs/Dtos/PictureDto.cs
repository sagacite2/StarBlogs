using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Blogs.Dtos
{
    [AutoMapFrom(typeof(Picture))]
    public class PictureDto : EntityDto
    {
        public int PostId { get; set; }
        public int StarId { get; set; }
        public int BlogId { get; set; }
        public string Url { get; set; }
    }
}
