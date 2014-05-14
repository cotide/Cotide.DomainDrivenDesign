using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security; 
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Tasks;
using Cotide.Domain.Dtos;
using Cotide.Domain.Enum;
using Cotide.Portal.Controllers.Controllers.Base;
using Cotide.Portal.Controllers.ViewModels.Login;
using Cotide.Portal.Controllers.ViewModels.Reg; 

namespace Cotide.Portal.Controllers.Controllers
{
    /// <summary>
    /// 用户注册/登陆
    /// </summary>
    public class RegController : BaseController
    {
        /// <summary>
        /// 用户查询
        /// </summary>
        private readonly IUserQueryService _userQueryService;

      

        /// <summary>
        /// 用户状态管理
        /// </summary>
        private readonly IIdentityTask _identityTask;

        #region IOC 注入

        /// <summary>
        /// 默认构造函数
        /// </summary> 
        /// <param name="userQueryService"></param>
        /// <param name="userTask"></param>
        /// <param name="identityTask"></param>
        public RegController(
            IUserQueryService userQueryService, 
           
            IIdentityTask identityTask)
        {
            this._userQueryService = userQueryService;
            
            _identityTask = identityTask;
        }

        #endregion

     
        
        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginViewModel viewModel)
        { 
            if (ModelState.IsValid)
            {
                var user = _userQueryService.FindOne(viewModel.UserName,viewModel.Paw);
                if (user != null)
                {
                    _identityTask.SignIn(user.Id, true, UserLoginRole.User);
                    return Redirect(Url.Action("Default", "Home"));
                }

                ModelState.AddModelError("", @"用户或者密码有误");
                return View();
            }
            ModelState.AddModelError("", @"非法的格式");
            return View(); 
        }

        public ActionResult Logoff()
        {
            _identityTask.SignOut();
            return Redirect(Url.Action("Default", "Home"));
        }
    }
}
