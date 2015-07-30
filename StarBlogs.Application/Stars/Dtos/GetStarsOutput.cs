using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace StarBlogs.Stars.Dtos
{
    public class GetStarsOutput:IOutputDto
    {
        public List<StarDto> Stars { get; set; }
    }
}
