using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Dtos;
using Cotide.Domain.Entity;
using Cotide.Portal.Controllers.Controllers.Base;

namespace Cotide.Portal.Controllers.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserInfoController : BaseController
    { 
        protected IUserQueryService UserQueryService;
        protected IUserInfoRepository UserInfoRepository;

        public UserInfoController(
            IUserQueryService userQueryService,
            IUserInfoRepository userInfoRepository
            )
        {
            UserQueryService = userQueryService;
            UserInfoRepository = userInfoRepository;
        }

        public ActionResult List()
        {
            var list = UserQueryService.FindAllPager(null,null,1,int.MaxValue);;
            return View(list);
        }

        public ActionResult Add(UserInfo userDto)
        {
            UserInfoRepository.Create(userDto);
            return  Redirect("List");
        }

        public ActionResult Delete(Guid userId)
        {
            var user = UserInfoRepository.FindAll().FirstOrDefault(x => x.Id == userId);
            if(user==null)
                return Redirect("List");

            UserInfoRepository.Delete(user);
            return Redirect("List");
        }
    }
}
