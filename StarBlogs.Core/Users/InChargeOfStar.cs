using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Users
{
    //表示用户拥有对某个明星的管理权限，前提是用户需要有CanManageTranslation权限
    public class InChargeOfStar:Entity
    {
        public int UserId { get; set; }
        public int StarId { get; set; }
    }
}
