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
    public class AdminLoginViewModel : UserLoginViewModel
    {
        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = @"您还没输入验证码")]
        [DisplayName(@" 验证码: ")]
        public string Captcha { get; set; }
    }
}
