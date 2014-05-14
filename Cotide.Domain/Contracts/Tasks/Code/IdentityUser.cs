using System;
using Cotide.Domain.Enum;

namespace Cotide.Domain.Contracts.Tasks.Code
{
    public class IdentityUser
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public IdentityUser()
        {
        }

        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }


        /// <summary>
        /// 用户名
        /// </summary> 
        public string UserName { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string RealName { get; set; }
  
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
         
        /// <summary>
        /// 用户角色
        /// </summary>
        public UserLoginRole UserLoginRole { get; set; }
    }
}
