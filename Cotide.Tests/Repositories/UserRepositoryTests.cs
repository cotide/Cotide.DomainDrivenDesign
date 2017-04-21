using System;
using System.ComponentModel.Composition.Hosting;
using System.Data.Entity;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Autofac; 
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Contracts.Repositories.Base;
using Cotide.Domain.Entity;
using Cotide.Domain.Enum;
using Cotide.Infrastructure.Context; 
using Cotide.Infrastructure.Context.Init;
using Cotide.Tests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cotide.Infrastructure.Repositories;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using Cotide.Domain;
using Cotide.Domain.Contracts.Tasks;

namespace Cotide.Tests.Repositories
{
    /// <summary>
    /// UserRepositoryWithIntType 的摘要说明
    /// </summary>  
    [TestClass()]
    public class UserRepository : TestBase
    {
        [TestMethod()]
        public void Init()
        {

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DefaultDbContext>());
            using (var context = new DefaultDbContext())
            {
                context.Database.Initialize(true);
                context.Add<UserInfo,Guid>(new UserInfo()
                {

                });
                context.SaveChanges();
            }

            // 初始化数据库
            // DbContextInitializer.Init();
            /*  var service1 = base.Get<IRepositoryWidthGuid<User>>();

              var service2 = base.Get<IRepositoryWidthGuid<User>>();

              var service3 = base.Get<IUserTask>();

              var service4 = base.Get<IUserQueryService>();*/

            /*var list = base.Get<IUserRepository>();

            // var lis3 = ComponentRegistrar.Get<IIdentityTask>();
            var list2 = base.Get<IUserRepository>();

            Console.WriteLine(list.Entities.Count());
            // Console.WriteLine(lis3.GetCurrentUser().UserName);
            Console.WriteLine(list2.Entities.Count());
        */
        }

        [TestMethod()]
        public void TTT()
        {
            //var DB = new DefaultDbContext();
            //var user = DB.UserInfo.FirstOrDefault();

            //IRepository<UserInfo, Guid> bll = base.Get<IRepository<UserInfo, Guid>>();
           
            //var obj = bll.FindAll().FirstOrDefault();
            //bll.Delete(obj);
           // IClientTask clientTask = base.Get<IClientTask>();


            var userInfoRepository = base.Get<IUserInfoRepository>();

            var clientRepository = base.Get<IClientRepository>();
      
            using (TransactionScope transaction = new TransactionScope())
            {
                 
                    userInfoRepository.Create(new UserInfo()
                    {
                        UserName = "ZZ"
                    });
                    
                    clientRepository.Create(new Client()
                    {
                        UserName = "AA"
                    });
              //  transaction.Complete();

            }

            /*
            EfUnitOfWorkContext bll = new EfUnitOfWorkContext();
            var content = bll.DbContext;
            content.Set<Admin>();

            EntityState state = content.Entry(new Admin
            {
                UserName = "测试"
            }).State;
            if (state == EntityState.Detached)
            {
                content.Entry(new Admin
                {
                    UserName = "测试"
                }).State = EntityState.Added;
              
            }
            content.SaveChanges();*/
        }

      
    }
}
