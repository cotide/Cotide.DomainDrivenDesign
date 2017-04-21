using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Enum;
using Cotide.Portal.Controllers.Controllers.Base;
using Cotide.Portal.Controllers.ViewModels.User;
using Cotide.QueryServices;
using Peacock.DataBase.Controllers.ApiControllers;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Cotide.Portal.Controllers.ApiControllers
{
    [WebApiPowerFilterAttrbute()]
    public class UserController : BaseApiController
    {

        public IClientAuthorizationQueryService ClientAuthorizationQueryService; 
        public IUserQueryService UserQueryService; 

        public UserController()
        {
            ClientAuthorizationQueryService = base.Get<IClientAuthorizationQueryService>() ;
            UserQueryService = base.Get<IUserQueryService>();
        }

        [HttpGet]
        public UserViewModel Get()
        { 
             
            var token = HttpContext.Current.Request.Headers["Token"];
            var tokenObj = ClientAuthorizationQueryService.Get(token);
            var userId = tokenObj.UserId;
            var user = UserQueryService.FindOne(userId);
            return new UserViewModel()
            {
                RealName = user.RealName,
                UserId = user.Id,
                UserName = user.UserName,
                UserRole = UserLoginRole.User
            };
        }
    }
}
