using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Cotide.Domain;
using Cotide.Domain.Contracts.Repositories.Base;
using Cotide.Framework.Extensions.Castle;
using Cotide.Framework.Extensions.WindsorService;
using Cotide.Infrastructure.Repositories;
using Cotide.Infrastructure.Repositories.Base;
using Cotide.Portal.Controllers.Controllers;
using Microsoft.Practices.ServiceLocation;

namespace Cotide.Portal.App_Start.CastleWindsor
{
    /// <summary>
    /// 依赖注入辅助类
    /// </summary>
    public class ComponentRegistrar
    {
        /// <summary>
        /// 注入规则
        /// </summary>
        /// <param name="container"></param>
        static void AddComponentsTo(IWindsorContainer container)
        {
            AddRepositoriesTo(container);
            AddQueryServiceTo(container);
            AddTasksTo(container);
        }

        /// <summary>
        /// 注入Repositories实例
        /// </summary>
        /// <param name="container"></param>
        static void AddRepositoriesTo(IWindsorContainer container)
        { 
            container.Register(
                AllTypes
                    .FromAssemblyNamed("Cotide.Infrastructure").Pick()
                    .WithService.AllInterfaces());  
        }


        /// <summary>
        /// 注入Task实例
        /// </summary>
        /// <param name="container"></param>
        static void AddTasksTo(IWindsorContainer container)
        {
            container.Register(
              AllTypes
                  .FromAssemblyNamed("Cotide.Tasks").Pick()
                  .WithService.AllInterfaces());
        }

        /// <summary>
        /// 注入QueryService实例
        /// </summary>
        /// <param name="container"></param>
        static void AddQueryServiceTo(IWindsorContainer container)
        {
            container.Register(
               AllTypes
                  .FromAssemblyNamed("Cotide.QueryServices").Pick()
                  .WithService.AllInterfaces());
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            IWindsorContainer container = new WindsorContainer();
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
            container.RegisterControllers(typeof(HomeController).Assembly);
            ComponentRegistrar.AddComponentsTo(container);
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }

        /// <summary>
        /// 获取接口实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Current.GetInstance<T>(); 
        }


        #region Helper

        /// <summary>
        ///  获取当前代理注入服务对象
        /// </summary>
        private static IServiceLocator Current
        {
            get
            {
                return ServiceLocator.Current;
            }
        }

        #endregion
    }
}
