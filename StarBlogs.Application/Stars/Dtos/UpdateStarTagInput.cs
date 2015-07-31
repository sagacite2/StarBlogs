using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Stars.Dtos
{
    public class UpdateStarTagInput : IInputDto
    {
        public int StarId { get; set; }
        public List<StarTagDto> StarTags { get; set; }
    }
}
