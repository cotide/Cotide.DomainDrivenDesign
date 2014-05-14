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
using Cotide.Portal.Controllers.ViewModels.SiteManager;

namespace Cotide.Portal.Controllers.Controllers.SiteManager
{
    /// <summary>
    /// 订单管理
    /// </summary>
    public class OrderController : BaseController
    { 

        #region IOC 注入

        /// <summary>
        /// 默认构造函数
        /// </summary> 
        /// <param name="orderQueryService">查询服务</param>
        /// <param name="orderTask">操作任务</param>
        public OrderController(
           )
        {
             
        }

        #endregion

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetOrderList()
        {
            return View("~/Views/SiteManager/Order/GetOrderList.cshtml");
        }

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetOrderList(OrderViewModel viewModel)
        {
         
            return View("~/Views/SiteManager/Order/GetOrderList.cshtml");
        }
    }
}
