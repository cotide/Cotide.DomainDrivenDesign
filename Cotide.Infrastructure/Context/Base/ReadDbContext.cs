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
    public  abstract class ReadDbContext<T> where T : DbContext, new()
    {
        protected DbContext Db;

        protected ReadDbContext()
        {
            Db = new T();
        } 


        public   IQueryable<TEntity> FindAll<TEntity, T1>() where TEntity : EntityByType<T1>
        {
            return Db.FindAll<TEntity, T1>();
        }


        public   IQueryable<TEntity> FindAll<TEntity>() where TEntity : EntityByType<long>
        {
            return Db.FindAll<TEntity>();
        }


        public TEntity FindOne<TEntity, T1>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityByType<T1>
        {
            return Db.FindOne<TEntity, T1>(predicate);
        }


        public  TEntity FindOne<TEntity>(long id) where TEntity : EntityByType<long>
        {
            return Db.FindOne<TEntity>(id);
        }


        public  TEntity FindOne<TEntity>(string id) where TEntity : EntityByType<string>
        {
            return Db.FindOne<TEntity>(id);
        }

        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <typeparam name="T1">返回结果集的类型</typeparam>
        /// <param name="spname">存储过程名</param>
        /// <param name="paras">存储过程参数</param>
        /// <returns></returns>
        public IList<T1> CallProcedure<T1>( 
           string spname,
           IDictionary<string, object> paras = null)
        {
            return Db.CallProcedure<T1>(spname, paras);
        }

        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <typeparam name="T1">返回结果集的类型</typeparam>
        /// <param name="spname">存储过程名</param>
        /// <param name="paras">存储过程参数</param>
        /// <param name="dicOutPar">带输出参数的值</param>
        /// <returns>返回指定类型的结果集</returns>
        public IList<T1> CallProcedure<T1>( 
            string spname,
            ref Dictionary<string, object> dicOutPar, 
            IDictionary<string, object> paras = null)
        {
            return Db.CallProcedure<T1>(spname, ref dicOutPar, paras);
        }
    }
}
