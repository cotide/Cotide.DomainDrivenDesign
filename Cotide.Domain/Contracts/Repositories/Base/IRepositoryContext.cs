using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Framework.UnitOfWork;

namespace Cotide.Domain.Contracts.Repositories.Base
{
    /// <summary>
    /// Repository Context  接口
    /// </summary>
    /// <summary>
    /// 
    /// </summary>
    public interface IRepositoryContext : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// 获取当前Domain唯一标示
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// 注册新的Domain
        /// </summary>
        /// <param name="obj">需要注册的Domain</param>
        void RegisterNew(object obj);

        /// <summary>
        /// 修改Domain
        /// </summary>
        /// <param name="obj">需要修改的Domain</param>
        void RegisterModified(object obj);


        /// <summary>
        /// 删除Domain
        /// </summary>
        /// <param name="obj">需要删除的Domain</param>
        void RegisterDeleted(object obj);
    }
}
