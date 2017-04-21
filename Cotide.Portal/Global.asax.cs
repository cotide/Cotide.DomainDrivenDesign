using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor; 
using Cotide.Domain.Contracts.Tasks;
using Cotide.Framework.Extensions;
using Cotide.Framework.Extensions.Castle;
using Cotide.Infrastructure.Context;
using Cotide.Infrastructure.Context.Init;
using Cotide.Infrastructure.Tool.Initialize;
using Cotide.Portal.App_Start;
using Cotide.Portal.Controllers.CastleWindsor;
using Microsoft.Practices.ServiceLocation;
using Peacock.DataBase.Web;

namespace Cotide.Portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        { 
            // WebApi注册
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // 初始化IOC
            ComponentRegistrar.Init();
            // Mapper规则注入
            MapperConfig.Register(); 
            // 初始化数据库
            DbContextInitializer.Init();
        }

  
        /// <summary>
        /// 用户权限验证处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated && !Request.IsStaticFile())
            { 
                var identityService = ComponentRegistrar.Get<IIdentityTask>(); 
                Context.User = identityService.GetCurrentUser(); 
            }
        }

    }
}