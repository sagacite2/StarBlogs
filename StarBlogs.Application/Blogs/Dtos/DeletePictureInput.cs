using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Blogs.Dtos
{
    public class DeletePictureInput : IInputDto
    {
        public int Id { get; set; }
    }
}
