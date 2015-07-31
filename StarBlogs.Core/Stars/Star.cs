using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using StarBlogs.Blogs;
using StarBlogs.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StarBlogs.Helpers;

namespace StarBlogs.Stars
{
    /// <summary>
    /// 明星
    /// </summary>
    [Table("Star")]
    public class Star : CreationAuditedEntity<int, User>
    {
       
        /// <summary>
        /// 明星原名（外文）
        /// </summary>
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 明星昵称、外号，可空
        /// </summary>
        public virtual string Nickname { get; set; }
        /// <summary>
        /// 明星的中文名
        /// </summary>
        public virtual string ChineseName { get; set; }
        public virtual Gender Gender { get; set; }
        // 头像图片地址，不一定需要记录到数据库
        //public virtual string PortraitUrl { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<StarTag> Tags { get; set; }
        /// <summary>
        /// 明星简介
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// 是否被屏蔽
        /// </summary>
        public virtual bool IsBlocked { get; set; }
        public virtual DateTime LastUpdateTime { get; set; }
    }
}
