using System;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using System.Collections.Generic;
using StarBlogs.Blogs;
using Abp.AutoMapper;
using StarBlogs.Helpers;
using Abp.Runtime.Validation;
using System.ComponentModel.DataAnnotations;
using StarBlogs.Blogs.Dtos;

namespace StarBlogs.Stars.Dtos
{
    [AutoMapFrom(typeof(Star))]
    public class StarDto : CreationAuditedEntityDto
    {
        [Required]
        [Range(2,30)]
        public string Name { get; set; }
        [Range(0, 20)]
        public string Nickname { get; set; }
        [Required]
        [Range(1, 20)]
        public string ChineseName { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public ICollection<BlogDto> Blogs { get; set; }
        public ICollection<StarTagDto> Tags { get; set; }
        [Range(0, 300)]
        public string Description { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public string CreatorUserName { get; set; }
    }
}
