using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cotide.Domain;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories.Base;
using Cotide.Domain.Contracts.Tasks;
using Cotide.Domain.Dtos;
using Cotide.Domain.Entity;
using Cotide.Portal.Controllers.Controllers.Base;

namespace Cotide.Portal.Controllers.Controllers
{

    public class HomeController : BaseController
    {
        protected IUserQueryService UserQueryService;
        protected IClientQueryService ClientQueryService;
  
        #region IOC 注入

        
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="userQueryService"></param>
        /// <param name="clientQueryService"></param>
        public HomeController(
            IUserQueryService userQueryService,
            IClientQueryService clientQueryService)
        {
            UserQueryService = userQueryService;
            ClientQueryService = clientQueryService;
        }

        #endregion

        [HttpGet]
        public ActionResult Default()
        {
            ViewData["user"] = CurrentUser;
            var viewModel =   ClientQueryService.FindAll();
            return View(viewModel); 
        } 
         
    }
}