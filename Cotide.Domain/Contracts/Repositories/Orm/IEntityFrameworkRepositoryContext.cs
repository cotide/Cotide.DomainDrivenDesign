using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.Repositories.Base;

namespace Cotide.Domain.Contracts.Repositories.Orm
{
    /// <summary>
    /// EntityFramework Repository Context 接口
    /// </summary>
    public interface IEntityFrameworkRepositoryContext : IRepositoryContext
    {
        /// <summary>
        /// 获取 <see cref="DbContext"/> 当前实例的 Entity Framework.
        /// </summary>
        DbContext Context { get; }
    }
}
