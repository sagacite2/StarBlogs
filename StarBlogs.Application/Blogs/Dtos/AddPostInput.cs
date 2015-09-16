﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Blogs.Dtos
{
    public class CreateUpdatePostInput : IInputDto
    {
        public int BlogId { get; set; }
      //  public Blog Blog { get; set; }
        //public int StarId { get; set; }
        //public virtual Star Star { get; set; }

        public string Content { get; set; }

      //  public ICollection<Picture> Pictures { get; set; }

      //  public DateTime PostTime { get; set; }

        public string DefaultTranslate { get; set; }

      //  public bool IsBlocked { get; set; }
    }
}
