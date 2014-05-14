using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Dtos;
using Cotide.Domain.Entity;
using Cotide.Framework.Mapper;
using Cotide.Tests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cotide.Tests.QueryService
{
    [TestClass()]
    public class ClientQueryServiceTest : TestBase
    {

        [TestMethod()]
        public void FindAll()
        {
      /*      var clientQueryService = base.Get<IClientQueryService>();
            var count = clientQueryService.FindAll();*/
            var bll = base.Get<IClientRepository>();
            var result = bll.FindAll().Select(x=>new ClientDto()
            {
                  ClientIdentifier =x.ClientIdentifier,
                   ClientSecret =x.ClientSecret,
                    CreateDateTime =x.CreateDateTime,
                     Id =x.Id,
                      LastUpdateDateTime =x.LastUpdateDateTime,
                       Name =x.Name,
                        Paw=x.Paw, RedirectUrl =x.RedirectUrl,
                         UserName=x.UserName
            }). ToList();
            Assert.IsTrue(result .Count> 0, "Pass");
        }
    }
}
