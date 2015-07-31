using System;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using System.Collections.Generic;
using StarBlogs.Stars;
using Abp.AutoMapper;

namespace StarBlogs.Blogs.Dtos
{
    [AutoMapFrom(typeof(Blog))]
    public class BlogDto : EntityDto
    {
        public string Url { get; set; }
        public BlogProvider Provider { get; set; }
        public string ProviderName { get; set; }
        public string Name { get; set; }
        public int StarId { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
