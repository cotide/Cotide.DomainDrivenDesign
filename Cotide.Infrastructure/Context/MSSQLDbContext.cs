using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using Cotide.Domain;
using Cotide.Domain.Entity;

namespace Cotide.Infrastructure.Context
{
    /// <summary>
    /// SQL数据库 数据访问上下文
    /// </summary> 
    [Export(typeof(DbContext))]
    public class MssqlDbContext : DbContext
    {
        public IDbConnectionFactory DbConnectionFactory;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MssqlDbContext(IDbConnectionFactory dbConnectionFactory)
        {
            DbConnectionFactory = dbConnectionFactory;
        }

         /// <summary>
         ///     初始化一个 使用连接名称为“default”的数据访问上下文类 的新实例
         /// </summary>
         public MssqlDbContext()
         {
             DbConnectionFactory.CreateConnection()
         }

        /// <summary>
        /// 初始化一个 使用指定数据连接名称或连接串 的数据访问上下文类 的新实例
        /// </summary>
        public MssqlDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString) {  }


        #region Domain Maping

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ShopCart> ShopCarts { get; set; }
         
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //移除一对多的级联删除约定，想要级联删除可以在 EntityTypeConfiguration<TEntity>的实现类中进行控制
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //多对多启用级联删除约定，不想级联删除可以在删除前判断关联的数据进行拦截
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
