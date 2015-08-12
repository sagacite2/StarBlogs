using Abp.Application.Services.Dto;
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
    public class TranslationDto : CreationAuditedEntityDto
    {
        public int OriginalPostId { get; set; }
        public int StarId { get; set; }
        public int BlogId { get; set; }
        public string Content { get; set; }
        public int VoteUp { get; set; }
        public int VoteDown { get; set; }
        public int InheritedEditionId { get; set; }
        public int CommentCount { get; set; }
    }
}
