using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using Cotide.Domain;
using Cotide.Domain.Entity;
using Cotide.Framework.Mapper;
using Cotide.Infrastructure.Config;

namespace Cotide.Infrastructure.Repositories.Context
{
    /// <summary>
    ///     EF数据访问上下文
    /// </summary> 
    public class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("MssqlDbConnString") { }

        public EFDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString) { }

        public EFDbContext(DbConnection existingConnection)
            : base(existingConnection, true) { }


        public DbSet<User> Users { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ShopCart> ShopCarts { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();  
            new UserMapper().RegistTo(modelBuilder.Configurations);
        }
    }
}
