using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using Cotide.Domain.Entity; 

namespace Cotide.Infrastructure.Tool.Configs
{
    internal sealed class Configuration : DbMigrationsConfiguration<EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EFDbContext context)
        {
            var user = new List<User>();
            user.Add(new User(){UserName = "AAA"});
            user.Add(new User() { UserName = "BBB" });
            DbSet<User> memberSet = context.Set<User>();
            memberSet.AddOrUpdate(m => user.ToArray());
        }
    }
}
