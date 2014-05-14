//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：EfUnitOfWorkContext.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/6/14 21:41:17 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Cotide.Infrastructure.Context;

namespace Cotide.Infrastructure.UnitOfWork
{
    public class EfUnitOfWorkContext : UnitOfWorkContextBase
    {
        /// <summary>
        /// 获取或设置 当前使用的数据访问上下文对象
        /// </summary>
        protected override DbContext Context
        {
            get { return MssqlDbContext; }
        }

        /// <summary>
        /// 获取或设置 默认的Demo项目数据访问上下文对象
        /// </summary> 
        public MssqlDbContext MssqlDbContext { get { return new MssqlDbContext(); }
        }
    }
}
