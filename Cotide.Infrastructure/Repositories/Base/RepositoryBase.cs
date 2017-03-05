using System.Data.Entity;
using Cotide.Infrastructure.Context;
using Cotide.Infrastructure.Context.Base;

namespace Cotide.Infrastructure.Repositories.Base
{
 
    public abstract class DefaultRepositoryBase
    { 
        /// <summary>
        /// 创建数据库对象
        /// </summary>
        /// <returns></returns>
        public DefaultDbContext NewDb()
        {
            return DefaultDbContext.Create();
        }


        //public ReadDbContext<T> NewReadDb<T>() where T : DbContext, new()
        //{
        
        //}
          
    }
}
