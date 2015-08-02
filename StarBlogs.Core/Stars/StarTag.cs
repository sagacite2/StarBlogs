using Abp.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Stars
{
    [Table("StarTag")]
    public class StarTag : Entity
    {
        public virtual int StarId { get; set; }
        public virtual int TagId { get; set; }
        [JsonIgnore]
        public virtual Star Star { get; set; }
        [JsonIgnore]
        [ForeignKey("TagId")]
        public virtual StarTagSetting Tag { get; set; }


    }
}
