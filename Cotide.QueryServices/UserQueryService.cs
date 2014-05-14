using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories.Base;
using Cotide.Domain.Dtos;
using Cotide.Domain.Entity;
using Cotide.Framework.Collections;
using Cotide.Framework.Mapper;
using Cotide.Framework.Utility;

namespace Cotide.QueryServices
{
    /// <summary>
    /// 用户查询服务
    /// </summary>
    public class UserQueryService : IUserQueryService
    {
        protected IRepository<UserInfo, Guid> UserDbProxyRepository;

        public UserQueryService(IRepository<UserInfo, Guid> userDbProxyRepository)
        {
            UserDbProxyRepository = userDbProxyRepository;
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="id">管理员ID</param>
        /// <returns></returns>
        public UserDto FindOne(Guid id)
        {  
            var query = (from user in UserDbProxyRepository.FindAll()
                         where user.Id ==id 
                         select user).SingleOrDefault();
            return query.MapTo< UserDto>();
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public UserDto FindOne(string userName)
        {
            var query = (from user in UserDbProxyRepository.FindAll()
                         where user.UserName == userName
                         select user).SingleOrDefault();
            return query.MapTo< UserDto>();
        }
 
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="paw">密码</param>
        /// <returns></returns>
        public UserDto FindOne(string userName, string paw)
        {
           // paw = CryptTools.Md5(paw);
            var query = (from user in UserDbProxyRepository.FindAll()
                         where user.UserName == userName
                         && user.Paw == paw
                         select user).SingleOrDefault();
            return query.MapTo<  UserDto>();
        }

        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public PagerList<UserDto> FindAllPager(
            string userName,
            string realName,
            int pageIndex,
            int pageSize)
        {
            var query = UserDbProxyRepository.FindAll();

            if (!string.IsNullOrEmpty(userName))
            {
                query = query.Where(x => x.UserName.Contains(userName));
            }
            if (!string.IsNullOrEmpty(realName))
            {
                query = query.Where(x => x.RealName.Contains(realName));
            }
            var count = query.Select(x => x.Id).Count();
            query = query.OrderBy(x => x.Id);
            var result = query.Skip(pageIndex <= 1 ? 0 : pageIndex).Take(pageSize).ToList();
            return new PagerList<UserDto>(result.Select(x => x.MapTo< UserDto>()), count, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="niceName">用户昵称</param>
        /// <returns></returns>
        public UserDto FindOneByNiceName(string niceName)
        {

            var query = (from user in UserDbProxyRepository.FindAll()
                         where user.RealName == niceName
                         select user).SingleOrDefault();
            return query.MapTo< UserDto>();
        }
         
    }
}
