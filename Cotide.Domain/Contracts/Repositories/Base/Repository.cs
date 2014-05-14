using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cotide.Domain.Entity.Base;
using Cotide.Framework.Collections;

namespace Cotide.Domain.Contracts.Repositories.Base
{
    /// <summary>
    /// Represents the base class for repositories.
    /// </summary>
    /// <typeparam name="TEntity">The type of the aggregate root.</typeparam>
    /// <typeparam name="T1"></typeparam>
    public abstract class Repository<TEntity,T1> : IRepository<TEntity,T1>
    where TEntity : EntityByType<T1> 
    {
        /// <summary>
        /// Initializes a new instance of <c>Repository&lt;TAggregateRoot&gt;</c> class.
        /// </summary>
        /// <param name="context">The repository context being used by the repository.</param>
        public Repository(IRepositoryContext context)
        {
           // this.Context = context;
        }

        public abstract IRepositoryContext Context { get; }
        public abstract TEntity GetByKey(object key);
        public abstract IEnumerable<TEntity> FindAll();
        public abstract void Create(TEntity aggregateRoot);
        public abstract void Delete(TEntity aggregateRoot);
        public abstract void Update(TEntity aggregateRoot);
        public abstract PagerList<TEntity> FindAll(
            Expression<Func<TEntity,
            dynamic>> sortPredicate,
            int pageIndex, 
            int pageSize);
    }
}
