using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using Cotide.Portal.Controllers.ViewModels.Login;

namespace Cotide.Portal.Controllers.ViewModels.Reg
{
    public class RegisterViewModel
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
        /// <summary>
        /// 用户昵称
        /// </summary>
        [Required(ErrorMessage = @"您还没输入用户昵称")]
        [DisplayName(@" 用户昵称: ")]
        [StringLength(20, ErrorMessage = @"用户账号格式:3-20个字符", MinimumLength = 3)]
        public string RealName { get; set; }

        /// <summary>
        /// 用户密码确认
        /// </summary>      
        [DataType(DataType.Password)]
        [Required(ErrorMessage = @"您还没输入密码确认")]
        [DisplayName(@"密码确认:")]
        [StringLength(20, ErrorMessage = @"密码格式：5-20个字符", MinimumLength = 5)]
        public string RepeatPaw { get; set; }

    }
}
