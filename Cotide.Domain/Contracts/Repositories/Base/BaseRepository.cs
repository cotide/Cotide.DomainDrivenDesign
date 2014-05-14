using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cotide.Domain.Contracts.Context.Provider;
using Cotide.Domain.Entity.Base;
using Cotide.Framework.Domain;
using Cotide.Framework.Utility;

namespace Cotide.Domain.Contracts.Repositories.Base
{
    /// <summary>
    /// 基类仓储对象
    /// </summary>
    public abstract class BaseRepository<TEntity, T1> :
        IRepository<TEntity, T1> where TEntity : EntityByType<T1>
    {
        /*  private readonly IDbSet<TEntity> _objectSet; */
        private readonly IUnitOfWorkContext<TEntity, T1> _unitOfWorkContext;

        /// <summary>
        /// 默认构造函数 
        /// </summary>
        /// <param name="objectSetProvider">注入<c>IUnitOfWorkContext</c>接口实例</param>
        protected BaseRepository(IUnitOfWorkContext<TEntity, T1> objectSetProvider)
        {

            // _objectSet = objectSetProvider.RegisterNew();
            if (objectSetProvider == null)
            {
                throw Guard.ThrowDataAccessException("初始化仓储BaseRepository发生异常：未知的IDbSetProvider实例");
            }
            _unitOfWorkContext = objectSetProvider;
        }


        /// <summary>
        ///     获取 当前实体的查询数据集
        /// </summary>
        public IQueryable<TEntity> FindAll()
        { 
            return _unitOfWorkContext.RegisterNew();
        }

        /// <summary>
        ///     获取 当前实体的查询数据集
        /// </summary>
        public IQueryable<TEntity> Query
        {
            get
            { 
                return _unitOfWorkContext.RegisterNew();
            }
        }
          

        /// <summary>
        ///     查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        public TEntity Get(T1 key)
        {
            Guard.CheckArgument(key, "key");
            return _unitOfWorkContext.Find(key);
        }

        /// <summary>
        ///    查找指定主键的实体记录
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public TEntity Get(Expression<Func<TEntity, bool>> whereClause)
        {
            return FindAll().Where(whereClause).FirstOrDefault();
        }

        /// <summary>
        ///     插入实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public int Create(TEntity entity, bool isSave = true)
        {
            Guard.IsNotNull(entity, "entity");
            _unitOfWorkContext.RegisterNew(entity);
            return isSave ? _unitOfWorkContext.Commit() : 0;
        }


        /// <summary>
        ///     批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public int Create(IEnumerable<TEntity> entities, bool isSave = true)
        {
            Guard.CheckArgument(entities, "entities");
            _unitOfWorkContext.RegisterNew(entities);
            return isSave ? _unitOfWorkContext.Commit() : 0;
        }

        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public int Delete(T1 id, bool isSave = true)  
        {
            Guard.CheckArgument(id, "id"); 
            TEntity entity = _unitOfWorkContext.Find(id);
            return entity != null ? Delete(entity, isSave) : 0;
        }

        /// <summary>
        ///     删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public int Delete(TEntity entity, bool isSave = true)
        {
            Guard.CheckArgument(entity, "entity");
            _unitOfWorkContext.RegisterDeleted(entity);
            return isSave ? _unitOfWorkContext.Commit() : 0;
        }

        /// <summary>
        ///     删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public int Delete(IEnumerable<TEntity> entities, bool isSave = true)
        { 
            Guard.CheckArgument(entities, "entities");
            _unitOfWorkContext.RegisterDeleted(entities);
            return isSave ? _unitOfWorkContext.Commit() : 0;
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true)
        {
            Guard.CheckArgument(predicate, "predicate");
            List<TEntity> entities = _unitOfWorkContext.DbContext.Set<TEntity>().Where(predicate).ToList();
            return entities.Count > 0 ? Delete(entities, isSave) : 0;
        }

        public int Update(TEntity entity, bool isSave = true)
        {
            Guard.CheckArgument(entity, "entity");
            _unitOfWorkContext.RegisterModified(entity);
            return isSave ? _unitOfWorkContext.Commit() : 0;
        }
    }
}
