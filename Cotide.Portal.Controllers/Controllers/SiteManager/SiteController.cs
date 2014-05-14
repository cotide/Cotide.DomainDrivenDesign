using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security; 
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Tasks;
using Cotide.Domain.Enum;
using Cotide.Framework.Utility;
using Cotide.Portal.Controllers.Controllers.Base;
using Cotide.Portal.Controllers.ViewModels.Login;
using Cotide.Portal.Controllers.ViewModels.Reg;

namespace Cotide.Portal.Controllers.Controllers.SiteManager
{
    /// <summary>
    /// 用户注册/登陆
    /// </summary> 
   //[Authorize]
    public class SiteController : BaseController
    { 

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
        public SiteController(
           IIdentityTask identityTask)
        {
             
            _identityTask = identityTask;
        }

        #endregion

        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult AdminLogin()
        {
            return View("~/Views/SiteManager/AdminLogin.cshtml");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(AdminLoginViewModel viewModel)
        {
            string sMsg = null;
            if (string.IsNullOrWhiteSpace(viewModel.UserName))
            {
                sMsg = "请输入用户名";
            }
            else if (string.IsNullOrWhiteSpace(viewModel.Paw))
            {
                sMsg = "请输入密码";
            }
            else if (string.IsNullOrWhiteSpace(viewModel.Captcha))
            {
                sMsg = "请输入验证码";
            }
            else if (Session["Captcha"] == null)
            {
                sMsg = "验证码已经超时，请刷新页面";
            }
            else if (Session["Captcha"].ToString() != viewModel.Captcha)
            {
                sMsg = "验证码填写错误";
            }
            if (!string.IsNullOrWhiteSpace(sMsg))
            {
                ModelState.AddModelError("AdminLogin", sMsg);
            }
            else
            {
                //var user = _userQueryService.FindOne(viewModel.UserName, viewModel.Paw);
                //if (user != null)
                //{
                //    _identityTask.SignIn(user.Id, true, UserLoginRole.Admin);
                //    return  RedirectToAction("Index");
                //}
                //sMsg = @"用户或者密码有误";
            }
            ViewBag.IMessage = sMsg;            
            return View("~/Views/SiteManager/AdminLogin.cshtml");
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns>跳转到登录页面</returns>
         [AllowAnonymous]
        public ActionResult Logoff()
        {
            _identityTask.SignOut();
            return RedirectToAction("AdminLogin");
        }

        /// <summary>
        /// 中部页面
        /// </summary>
        /// <returns>页面信息</returns>
        public ActionResult Main()
        {
            return View("~/Views/SiteManager/Main.cshtml");
        }

        public ActionResult Index()
        {
            return View("~/Views/SiteManager/Index.cshtml");
        }         

        /// <summary>
        /// 头部页面
        /// </summary>
        /// <returns>页面信息</returns>
        public ActionResult Top()
        {
            if (CurrentUser != null)
            {
                var showUser = string.IsNullOrWhiteSpace(CurrentUser.RealName)
                                   ? CurrentUser.RealName
                                   : CurrentUser.UserName;
                ViewBag.LoginInfo = " [" + showUser + "],欢迎您的光临！！！ 今天是" +
                    DateTime.Now.ToString("yyyy年M月d日") + DateHelper.GetWeekString();
                ViewBag.CurrentUser = CurrentUser;
            }
            return View("~/Views/SiteManager/Top.cshtml");
        }

        /// <summary>
        /// 底部页面
        /// </summary>
        /// <returns>页面信息</returns>
        public ActionResult Bottom()
        {
            return View("~/Views/SiteManager/Bottom.cshtml");
        }

        /// <summary>
        /// 左侧菜单导航
        /// </summary>
        /// <returns>页面信息</returns>
        public ActionResult Left()
        {
            return View("~/Views/SiteManager/Left.cshtml");
        }

        /// <summary>
        /// 分割页面
        /// </summary>
        /// <returns>页面信息</returns>
        public ActionResult Middle()
        {
            return View("~/Views/SiteManager/Middle.cshtml");
        }
    }
}
