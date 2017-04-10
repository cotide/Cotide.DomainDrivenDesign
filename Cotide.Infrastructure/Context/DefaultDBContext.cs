using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Cotide.Domain.Entity;
using Cotide.Framework.Domain;
using Cotide.Framework.EF;
using Cotide.Framework.Utility;
using Cotide.Infrastructure.Context.Base;
using Cotide.Infrastructure.Mapper;

namespace Cotide.Infrastructure.Context
{
    /// <summary>
    /// 默认的DbContext 上下文
    /// </summary> 
    public class DefaultDbContext : ReadWriteDbContext 
    {
        /// <summary>
        /// EF 上下文
        /// </summary>
        /// <returns></returns>
        public static DefaultDbContext Create()
        {
            return new DefaultDbContext();
        }
         
      
        /// <summary>
        /// Fluent 方式配置Domain
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //new UserInfoMapper().RegistTo(modelBuilder.Configurations);
            //new ClientMapper().RegistTo(modelBuilder.Configurations);
            //new ClientAuthorizationMapper().RegistTo(modelBuilder.Configurations); 
        }
         
    }
}
