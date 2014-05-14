using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using Cotide.Domain.Dtos;
using Cotide.Domain.Entity;
using Cotide.Framework.Collections;
  

namespace Cotide.Domain.Contracts.QueryServices
{
    /// <summary>
    /// 用户查询接口
    /// </summary>
    public interface IUserQueryService  
    {
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        UserDto FindOne(Guid id);

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName">用户ID</param>
        /// <returns></returns>
        UserDto FindOne(string userName);
         

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="paw">密码</param>
        /// <returns></returns>
        UserDto FindOne(string userName, string paw);

        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        PagerList<UserDto> FindAllPager(string userName, string realName, int pageIndex, int pageSize);

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="niceName">用户昵称</param>
        /// <returns></returns>
        UserDto FindOneByNiceName(string niceName);
    }
}
