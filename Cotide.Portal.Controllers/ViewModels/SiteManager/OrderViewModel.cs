using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Cotide.Portal.Controllers.ViewModels.SiteManager
{
    /// <summary>
    /// 用户视图
    /// </summary>
    public class OrderViewModel : PagingViewModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [DisplayName(@" 用户名: ")]
        public string UserName { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [DisplayName(@" 用户昵称: ")]
        public string RealName { get; set; }
        
    }
}
