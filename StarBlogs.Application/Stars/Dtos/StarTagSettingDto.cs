using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Stars.Dtos
{
     [AutoMapFrom(typeof(StarTagSetting))]
    public class StarTagSettingDto : EntityDto
    {
        public string TagName { get; set; }
        public int ParentTagId { get; set; }
    }
}
