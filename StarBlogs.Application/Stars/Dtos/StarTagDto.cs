using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Stars.Dtos
{
    [AutoMapFrom(typeof(StarTag))]
    public class StarTagDto : EntityDto
    {
        public int StarId { get; set; }
        public int TagId { get; set; }
    }
}
