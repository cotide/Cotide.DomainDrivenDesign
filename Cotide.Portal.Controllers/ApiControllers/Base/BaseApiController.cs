using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cotide.Domain.Contracts.Tasks.Code;
using Cotide.Domain.Dtos;
using Cotide.Framework.Utility;
using Cotide.Portal.Controllers.CastleWindsor;
using Cotide.Portal.Controllers.Core;
using Cotide.Portal.Controllers.ViewModels.User;
using Microsoft.Practices.ServiceLocation;

namespace Cotide.Portal.Controllers.Controllers.Base
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class BaseApiController : System.Web.Http.ApiController
    {

        /// <summary>
        /// 接口实例化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T Get<T>()
        {
            return ComponentRegistrar.Get<T>(); 
        } 

        ///// <summary>
        ///// 当前登录用户
        ///// </summary>
        //protected virtual UserViewModel CurrentUser
        //{
        //    get
        //    {
        //        var user = HttpContext.Current.User as UserPrincipal;
        //        return user == null ? null : CreateUserViewModel(user);
        //    }
        //}

        ///// <summary>
        ///// 判断当前用户是否已登录
        ///// </summary>
        //protected virtual bool IsLogin
        //{
        //    get { return CurrentUser != null; }

        //}
 
        //#region Helper

        //private UserViewModel CreateUserViewModel(UserPrincipal userPrincipal)
        //{
        //    return new UserViewModel()
        //        {
        //            RealName = userPrincipal.RealName,
        //            UserId = userPrincipal.UserId,
        //            UserName = userPrincipal.UserName,
        //            UserRole = userPrincipal.UserRole
        //        };
        //}

       
        //#endregion
    }
}
