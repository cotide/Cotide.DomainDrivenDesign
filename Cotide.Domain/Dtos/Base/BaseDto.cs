using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Entity.Base;
using Cotide.Framework.Domain;

namespace Cotide.Domain.Dtos.Base
{
    /// <summary>
    /// 基类Dto
    /// </summary>
    public class BaseDto<T>  
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected BaseDto()
        {           
            CreateDateTime = DateTime.Now;
            LastUpdateDateTime = DateTime.Now;
        } 
  

        /// <summary>
        /// 主键
        /// </summary> 
        public virtual T Id { get; set; }

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
