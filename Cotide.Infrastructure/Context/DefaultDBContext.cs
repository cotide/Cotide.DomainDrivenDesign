using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Cotide.Domain.Entity;
using Cotide.Framework.Utility;
using Cotide.Infrastructure.Mapper;

namespace Cotide.Infrastructure.Context
{
    /// <summary>
    /// 默认的DbContext 上下文
    /// </summary> 
    public class DefaultDbContext : DbContext
    {
        public IDbSet<Client> Data { get; set; }

        public IDbSet<UserInfo> UserInfo { get; set; }

        public IDbSet<ClientAuthorization> Message { get; set; } 
         
        public DefaultDbContext()
            : base("default")
        {
        }


        public DefaultDbContext(DbConnection existingConnection)
            : base(existingConnection, true)
        {
             
        }  
        /// <summary>
        /// Fluent 方式配置Domain
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            new UserInfoMapper().RegistTo(modelBuilder.Configurations);
            new ClientMapper().RegistTo(modelBuilder.Configurations);
            new ClientAuthorizationMapper().RegistTo(modelBuilder.Configurations); 
        }
         
    }
}
