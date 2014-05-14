using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using AutoMapper; 
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories.Base;
using Cotide.Domain.Contracts.Tasks;
using Cotide.Domain.Dtos;
using Cotide.Domain.Entity;
using Cotide.Domain.Enum;
using Cotide.Framework.Config;
using Cotide.Framework.Mapper;
using Cotide.Framework.Utility;
using Cotide.Infrastructure.Context;
using Cotide.Infrastructure.Context.Init;
using Cotide.Infrastructure.Tool.Initialize;
using Cotide.Tests.Base; 
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework; 

namespace Cotide.Tests.Tools
{
    /// <summary>
    /// SQL数据库 数据访问上下文
    /// </summary>  
    [TestClass()] 
    public class MssqlDbContextTests : TestBase
    {
        /// <summary>
        /// 数据库初始化
        /// </summary> 
       [TestInitialize]
        public void DatabaseInitTest()
        { 
           
            Database.SetInitializer(
                   new DropCreateDatabaseIfModelChanges<DefaultDbContext>());
            using (var context = new DefaultDbContext())
            { 
                context.Database.Initialize(true);
            }
            Console.WriteLine("数据库初始化完毕!");
        }


         [TestMethod()]
        public void DbInit()
        {
            Console.WriteLine("生成数据库成功");
        }

         [TestMethod()]
         public void TestDb()
        {
            
        }


        [TestMethod()]
        public void Query()
        {

            var userQueryService = base.Get<IUserQueryService>();  
            var result = userQueryService.FindAllPager("", "", 2, 2);
            Console.WriteLine("总页数"+result.TotalCount);
            foreach (var userDto in result)
            {
                Console.WriteLine(userDto.Id);
            }
        }

    }
}
