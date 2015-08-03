using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Blogs.Dtos
{
    public class BlockPostInput : IInputDto
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }
    }
}
