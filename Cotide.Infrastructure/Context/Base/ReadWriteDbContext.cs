using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Cotide.Framework.Domain;
using Cotide.Framework.Extensions;

namespace Cotide.Infrastructure.Context.Base
{
    public abstract class ReadWriteDbContext  : ReadDbContext 
    {

        protected ReadWriteDbContext()   
        {

        }




        /// <summary>
        /// 批量新增实体
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="T1">主键类型</typeparam>
        /// <param name="entities">实体值</param>
        public void Add<TEntity, T1>(params TEntity[] entities) where TEntity : EntityByType<T1>
        {
            if (entities == null) throw new ArgumentNullException("entities");
            foreach (TEntity entity in entities)
            {
                IDbSet<TEntity> dbSet = Mapper<TEntity, T1>();
                try
                {
                    DbEntityEntry<TEntity> entry = this.Entry(entity);
                    if (entry.State == EntityState.Detached)
                    {
                        dbSet.Attach(entity);
                        entry.State = EntityState.Added;
                    }
                }
                catch (InvalidOperationException)
                {
                    TEntity oldEntity = dbSet.Find(entity.Id);
                    this.Entry(oldEntity).CurrentValues.SetValues(entity);
                }
            }
        }


        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="T1">主键类型</typeparam>
        /// <param name="entities">实体值</param>
        public void Remove<TEntity, T1>(params TEntity[] entities) where TEntity : EntityByType<T1>
        {
            if (entities == null) throw new ArgumentNullException("entities");
            foreach (TEntity entity in entities)
            {
                IDbSet<TEntity> dbSet = Mapper<TEntity, T1>();
                try
                {
                    DbEntityEntry<TEntity> entry = this.Entry(entity);
                    if (entry.State == EntityState.Detached)
                    {
                        dbSet.Attach(entity);
                        entry.State = EntityState.Deleted;
                    }
                }
                catch (InvalidOperationException)
                {
                    TEntity oldEntity = dbSet.Find(entity.Id);
                    this.Entry(oldEntity).CurrentValues.SetValues(entity);
                }
            }
        }





        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="T1">主键类型</typeparam>
        /// <param name="entities">实体值</param>
        public void Update<TEntity, T1>(
            params TEntity[] entities)
            where TEntity : EntityByType<T1>
        {
            if (entities == null) throw new ArgumentNullException("entities");

            foreach (TEntity entity in entities)
            {
                DbSet<TEntity> dbSet = this.Set<TEntity>();
                try
                {
                    DbEntityEntry<TEntity> entry = this.Entry(entity);
                    if (entry.State == EntityState.Detached)
                    {
                        dbSet.Attach(entity);
                        entry.State = EntityState.Modified;
                    }
                }
                catch (InvalidOperationException)
                {
                    TEntity oldEntity = dbSet.Find(entity.Id);
                    this.Entry(oldEntity).CurrentValues.SetValues(entity);
                }
            }
        }

        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="T1">主键类型</typeparam>
        /// <param name="propertyExpression"></param>
        /// <param name="entities">实体值</param>
        public void Update<TEntity, T1>(
            Expression<Func<TEntity, object>> propertyExpression,
            params TEntity[] entities)
            where TEntity : EntityByType<T1>
        {
            if (propertyExpression == null) throw new ArgumentNullException("propertyExpression");
            if (entities == null) throw new ArgumentNullException("entities");
            System.Collections.ObjectModel.ReadOnlyCollection<MemberInfo> memberInfos = ((dynamic)propertyExpression.Body).Members;
            foreach (TEntity entity in entities)
            {
                DbSet<TEntity> dbSet = this.Set<TEntity>();
                try
                {
                    DbEntityEntry<TEntity> entry = this.Entry(entity);
                    entry.State = EntityState.Unchanged;
                    foreach (var memberInfo in memberInfos)
                    {
                        entry.Property(memberInfo.Name).IsModified = true;
                    }
                }
                catch (InvalidOperationException)
                {
                    TEntity originalEntity = dbSet.Local.Single(m => Equals(m.Id, entity.Id));
                    ObjectContext objectContext = ((IObjectContextAdapter)this).ObjectContext;
                    ObjectStateEntry objectEntry = objectContext.ObjectStateManager.GetObjectStateEntry(originalEntity);
                    objectEntry.ApplyCurrentValues(entity);
                    objectEntry.ChangeState(EntityState.Unchanged);
                    foreach (var memberInfo in memberInfos)
                    {
                        objectEntry.SetModifiedProperty(memberInfo.Name);
                    }
                }
            }
        }

    }
}
