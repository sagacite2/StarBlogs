using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using StarBlogs.Blogs;
using StarBlogs.Helpers;

namespace StarBlogs.Stars.Dtos
{
    public class CreateUpdateStarInput:IInputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string ChineseName { get; set; }
        public Gender Gender { get; set; }
        public string Description { get; set; }
        public bool IsBlocked { get; set; }
        public string BlogUrl { get; set; }
        public override string ToString()
        {
            return "CreateUpdateStarInput:Name='"+Name+"' Nickname='"+Nickname+"' ChineseName='"+ChineseName+"' Sex='"
                + EnumDescription.GetDescription(typeof(Gender), Gender) + "'\r\nDescription : "
                + Description+"\r\nBlogUrl='"+BlogUrl+"'";
        }
    }
}
