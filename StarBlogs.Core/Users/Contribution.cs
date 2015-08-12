using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Users
{

    //网友在某个标签下的翻译贡献度
     [Table("Contribution")]
    public class Contribution : Entity
    {
         public int UserId { get; set; }
         public User User { get; set; }
         public int TagId { get; set; }
         public int Count { get; set; }
    }
}
