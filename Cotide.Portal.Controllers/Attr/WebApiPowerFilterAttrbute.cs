 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc; 
using System.Configuration;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Portal.App_Start.CastleWindsor;
using Microsoft.Practices.ServiceLocation;

namespace Peacock.DataBase.Controllers.ApiControllers
{
    /// <summary>
    /// WebApi权限验证
    /// </summary>
    /// <remarks>
    ///     <para>    Creator：LHC</para>
    ///     <para>CreatedTime：2013/11/20 9:52:32</para>
    /// </remarks>	
    public class WebApiPowerFilterAttrbute : AuthorizationFilterAttribute
    {

        public IClientAuthorizationQueryService ClientAuthorizationQueryService;

        public WebApiPowerFilterAttrbute()
        {

            ClientAuthorizationQueryService = ComponentRegistrar.Get<IClientAuthorizationQueryService>();
        } 

 

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            if (!actionContext.Request.Headers.Contains("Token"))
            {
                NoPowerHttpsRequest(actionContext);
            }
            else
            {
                var token = actionContext.Request.Headers.GetValues("Token").FirstOrDefault();
                // 检查Token
                var tokenObj = ClientAuthorizationQueryService.Get(token);
                if (tokenObj == null)
                {
                    NoPowerHttpsRequest(actionContext);
                }
            } 
        }

        protected virtual void NoPowerHttpsRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateErrorResponse(
                       HttpStatusCode.Forbidden, "您无权限访问!");
        }
    }
}
