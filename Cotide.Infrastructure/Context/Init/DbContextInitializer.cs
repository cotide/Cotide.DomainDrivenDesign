using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Cotide.Domain.Entity;
using Cotide.Framework.Config;
using Cotide.Framework.Utility;

namespace Cotide.Infrastructure.Context.Init
{
    /// <summary>
    /// 初始化数据库策略
    /// </summary>
    public class DbContextInitializer  
    {
        public static void Init()
        {
            // 初始化数据库规则
            Database.SetInitializer<DefaultDbContext>(null); 
        }

    }
} 