using System;
using System.Collections.Generic;
using System.Data.Entity;
using Cotide.Domain.Entity.Base;

namespace Cotide.Infrastructure.UnitOfWork.Impl
{
    /// <summary>
    /// 数据单元操作接口
    /// </summary>
    public interface IUnitOfWorkContext : IUnitOfWork, IDisposable
    {
        /// <summary>
        ///   为指定的类型返回 System.Data.EntityWidthIntType.DbSet，这将允许对上下文中的给定实体执行 CRUD 操作。
        /// </summary>
        /// <typeparam name="TEntity"> 应为其返回一个集的实体类型。 </typeparam>
        /// <typeparam name="T1">主键类型</typeparam>
        /// <returns> 给定实体类型的 System.Data.EntityWidthIntType.DbSet 实例。 </returns>
        DbSet<TEntity> Set<TEntity, T1>() where TEntity : EntityByType<T1>;

        /// <summary>
        ///   注册一个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <typeparam name="T1">主键类型</typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterNew<TEntity, T1>(TEntity entity) where TEntity : EntityByType<T1>;

        /// <summary>
        ///   批量注册多个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <typeparam name="T1">主键类型</typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        void RegisterNew<TEntity,T1>(IEnumerable<TEntity> entities) where TEntity : EntityByType<T1>;

        /// <summary>
        ///   注册一个更改的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <typeparam name="T1">主键类型</typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterModified<TEntity, T1>(TEntity entity) where TEntity : EntityByType<T1>;

        /// <summary>
        ///   注册一个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <typeparam name="T1">主键类型</typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterDeleted<TEntity, T1>(TEntity entity) where TEntity : EntityByType<T1>;

        /// <summary>
        ///   批量注册多个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <typeparam name="T1">主键类型</typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        void RegisterDeleted<TEntity, T1>(IEnumerable<TEntity> entities) where TEntity : EntityByType<T1>;
    }
}
