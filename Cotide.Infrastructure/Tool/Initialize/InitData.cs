using System.Collections.Generic;
using System.Data.Entity;
using Cotide.Domain.Entity; 
using Cotide.Domain;

namespace Cotide.Infrastructure.Tool.Initialize
{
  /*  /// <summary>
    /// 初始化数据
    /// </summary>
    public class InitData : CreateDatabaseIfNotExists<MssqlDbContext>
    {
        protected override void Seed(MssqlDbContext context)
        {
            var members = new List<User>
            {
                new User { UserName = "admin", Paw = "admin"},
                new User { UserName = "user", Paw = "user"}
            };
            DbSet<User> memberSet = context.Set<User>();
            members.ForEach(m => memberSet.Add(m));
            context.SaveChanges();
        }
    }*/
}
