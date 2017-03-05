using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Cotide.Portal.App_Start;
using Cotide.Portal.App_Start.CastleWindsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Cotide.Tests.Base
{
    /// <summary>
    /// 测试基类
    /// </summary>  
    [TestClass()]
    public class TestBase
    {
        [TestInitialize]
        public virtual void SetUp()
        {
            ComponentRegistrar.Init();
            // Mapper规则注入
            MapperConfig.Register();
            // HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
        }

        /// <summary>
        /// 获取接口实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T Get<T>()
        {
            return ComponentRegistrar.Get<T>();
        }


    }
}
