using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cotide.Domain.Entity.Base
{
    /// <summary>
    /// 实体抽象基类
    /// </summary>
    /// <typeparam name="T1"></typeparam> 
    public abstract class EntityByType<T1>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected EntityByType()
        {
            CreateDateTime = DateTime.Now;
            LastUpdateDateTime = DateTime.Now;
        }

        /// <summary>
        /// 主键
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual T1 Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime? LastUpdateDateTime { get; set; }
    }
}
