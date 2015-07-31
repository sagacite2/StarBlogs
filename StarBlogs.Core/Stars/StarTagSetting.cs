using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Stars
{
    [Table("StarTagSetting")]
    public class StarTagSetting : Entity
    {
        public virtual string TagName { get; set; }
        public virtual int ParentTagId { get; set; }
    }
}
