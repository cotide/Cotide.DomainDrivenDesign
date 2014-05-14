using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Cotide.Portal.Controllers.ViewModels.Login
{
    /// <summary>
    /// 用户视图
    /// </summary>
    public class UserLoginViewModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = @"您还没输入账号")]
        [DisplayName(@" 用户名: ")]
        [StringLength(50, ErrorMessage = @"用户账号格式:5-20个字符", MinimumLength = 5)] 
        public string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [DataType(DataType.Password)]
        [Required(ErrorMessage = @"您还没输入密码")]
        [DisplayName(@"密码:")]
        [StringLength(20, ErrorMessage = @"密码格式：5-20个字符", MinimumLength = 5)] 
        public string Paw { get; set; }
    }
}
