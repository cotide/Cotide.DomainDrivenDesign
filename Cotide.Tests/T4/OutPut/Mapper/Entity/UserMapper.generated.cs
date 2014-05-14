﻿ 
using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
 
using Cotide.Domain.Entity; 
using Cotide.Framework.Mapper;

namespace Cotide.Infrastructure.Mapper 
{
    /// <summary>
    /// 实体类-数据表映射——User
    /// </summary>    
	internal partial class UserMapper : EntityTypeConfiguration<User>, IEntityMapper
    {
        /// <summary>
        /// 实体类-数据表映射构造函数——User
        /// </summary>
        public UserMapper()
        {
			UserConfigurationAppend();
        }
		
        /// <summary>
        /// 额外的数据映射
        /// </summary>
        partial void UserConfigurationAppend();
		
        /// <summary>
        /// 将当前实体映射对象注册到当前数据访问上下文实体映射配置注册器中
        /// </summary>
        /// <param name="configurations">实体映射配置注册器</param>
        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}