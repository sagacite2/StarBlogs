using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Stars.Dtos
{
    public class CreateTagSettingInput : IInputDto
    {
        public string Name { get; set; }
    }
    public class UpdateTagSettingInput : IInputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
