﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Blogs.Dtos
{
     [AutoMapFrom(typeof(OriginalPost))]
    public class PostDto:EntityDto
    {
        public int BlogId { get; set; }
       
        public int StarId { get; set; }

        public string Content { get; set; }

        public ICollection<PictureDto> Pictures { get; set; }

        public DateTime PostTime { get; set; }

        public string DefaultTranslate { get; set; }
        public bool IsBlocked { get; set; }
    }
}
