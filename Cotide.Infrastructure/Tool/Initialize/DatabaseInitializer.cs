using System.Data.Entity;
using Cotide.Domain.Entity;

namespace Cotide.Infrastructure.Tool.Initialize
{
    /// <summary>
    /// 数据库初始化操作类
    /// </summary>
    public static class DatabaseInitializer
    { 
        /// <summary>
        /// 数据库初始化
        /// </summary>
        public static void Initialize<TContent>() where TContent : DbContext
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TContent>());  
        }
    }


 
}
