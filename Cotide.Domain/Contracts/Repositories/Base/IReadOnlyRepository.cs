using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cotide.Domain.Entity.Base;
using Cotide.Framework.Domain;

namespace Cotide.Domain.Contracts.Repositories.Base
{
    /// <summary>
    ///     定义仓储模型中的数据标准操作
    /// </summary>
    /// <typeparam name="TEntity">动态实体类型</typeparam>
    /// <typeparam name="T1">主键类型</typeparam>
    public interface IReadOnlyRepository<TEntity, in T1> where TEntity : EntityByType<T1>
    {
        /// <summary>
        ///     获取 当前实体的查询数据集
        /// </summary>
        IQueryable<TEntity> Query { get; }

        /// <summary>
        ///     获取 当前实体的查询数据集
        /// </summary>
        IQueryable<TEntity> FindAll();
          
        /// <summary>
        ///     查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        TEntity Get(T1 key);

        /// <summary>
        ///    查找指定主键的实体记录
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> whereClause);
    }


/*
    /// <summary>
    ///     定义仓储模型中的数据标准操作
    /// </summary>
    /// <typeparam name="TEntity">动态实体类型</typeparam> 
    public interface IReadOnlyRepositoryWidthGuid<TEntity> 
        : IReadOnlyRepository<TEntity, Guid> where TEntity : EntityByType<Guid> 
    {
        
    }

    /// <summary>
    ///     定义仓储模型中的数据标准操作
    /// </summary>
    /// <typeparam name="TEntity">动态实体类型</typeparam> 
    public interface IReadOnlyRepositoryWidthInt<TEntity>
        : IReadOnlyRepository<TEntity, int> where TEntity : EntityByType<int>
    {

    }*/

}
