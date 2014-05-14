using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Castle.Components.DictionaryAdapter;

namespace Cotide.Framework.Domain
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
        [System.ComponentModel.DataAnnotations.Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual T1 Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary> 
        [Column(TypeName = "datetime2")]
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary> 
        [Column(TypeName = "datetime2")]
        public DateTime? LastUpdateDateTime { get; set; }

        
    }
}
