using System;
using System.Data.Entity;
using Cotide.Domain.Contracts.Context; 
using Cotide.Framework.Domain;

namespace Cotide.Infrastructure.Context.Adapter
{
    /// <summary>
    /// DBContext提供者
    /// </summary> 
    public  class EfUnitOfWorkContext  : UnitOfWorkContextBase
    {

        protected static Lazy<DefaultDbContext> DefaultDbContext; 
        private static readonly object synRoot = new object(); 
        public EfUnitOfWorkContext()
        {
            if (DefaultDbContext == null)
            {
                lock (synRoot)
                {
                    if (DefaultDbContext == null)
                    {
                        DefaultDbContext = new Lazy<DefaultDbContext>();
                    }
                }
            }
        }


        /// <summary>
        ///     获取 当前使用的数据访问上下文对象
        /// </summary>
        protected override DbContext Context
        {
            get
            {
                if (DefaultDbContext != null)
                    return DefaultDbContext.Value;
                else
                {
                    var defaultDbContext = new Lazy<DefaultDbContext>();
                    return defaultDbContext.Value;
                } 
            }
        } 
    }
}
