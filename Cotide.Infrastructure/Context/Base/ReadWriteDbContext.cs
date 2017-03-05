using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cotide.Framework.Domain;
using Cotide.Framework.Extensions;

namespace Cotide.Infrastructure.Context.Base
{
    public abstract class ReadWriteDbContext<T> : ReadDbContext<T> where T : DbContext, new()
    {


        public void Add<TEntity, T1>(params TEntity[] entities) where TEntity : EntityByType<T1>
        {
            Db.Add<TEntity, T1>(entities);
        }


        public void Update<TEntity, T1>(
            params TEntity[] entities) where TEntity : EntityByType<T1>
        {
            Db.Update<TEntity, T1>(entities);
        }

  
        public void Update<TEntity, T1>(
            Expression<Func<TEntity, object>> propertyExpression,
            params TEntity[] entities)
            where TEntity : EntityByType<T1>
        {
            Db.Update<TEntity, T1>(propertyExpression, entities);
        }

    }
}
