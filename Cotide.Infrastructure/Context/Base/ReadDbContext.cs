using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Cotide.Framework.Domain;
using Cotide.Framework.EF;
using Cotide.Framework.Extensions;

namespace Cotide.Infrastructure.Context.Base
{
    /// <summary>
    /// 只读 DbContext
    /// </summary>
    public abstract class ReadDbContext : DbContext 
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected ReadDbContext()
            : base("default")
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="existingConnection">数据库连接对象</param>
        protected ReadDbContext(DbConnection existingConnection)
            : base(existingConnection, true)
        {

        } 

        /// <summary>
        /// 实体映射
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <typeparam name="T1">主键类型</typeparam>
        /// <returns></returns>
        protected IDbSet<TEntity> Mapper<TEntity, T1>()
            where TEntity : EntityByType<T1>
        {
            return   base.Set<TEntity>();
        }


        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <typeparam name="T1">主键类型</typeparam>
        /// <returns></returns>
        public IQueryable<TEntity> FindAll<TEntity, T1>()
            where TEntity : EntityByType<T1>
        {
            return Mapper<TEntity, T1>();
        }


        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="T1">主键类型</typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity FindOne<TEntity, T1>(
            Expression<Func<TEntity, bool>> predicate)
            where TEntity : EntityByType<T1>
        {
            return Mapper<TEntity, T1>().Where(predicate).FirstOrDefault();
        }


        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="id">主键类型</param>
        /// <returns></returns>
        public TEntity FindOne<TEntity>(int id) where TEntity : EntityByType<int>
        {
            return Mapper<TEntity, int>().FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="id">主键类型</param>
        /// <returns></returns>
        public TEntity FindOne<TEntity>(string id) where TEntity : EntityByType<string>
        {
            return Mapper<TEntity, string>().FirstOrDefault(x => x.Id == id);
        }


        /// <summary>
        /// 转换成ObjectContext
        /// </summary> 
        /// <returns></returns> 
        public ObjectContext ToExcuteSqlDbContext()
        {
            return ((IObjectContextAdapter)this).ObjectContext;
        }


        /// <summary>
        /// 通过Castle调用存储过程
        /// </summary> 
        /// <param name="spname">存储过程名</param>
        /// <param name="paras">存储过程参数</param>
        /// <returns>返回结果集</returns>
        public bool ExecuteProcedure(
            string spname,
            IDictionary<string, object> paras)
        {
            var dicOutPar = new Dictionary<string, object>();
            return ExecuteProcedure(spname, paras, ref dicOutPar);
        }


        /// <summary>
        /// 通过Castle调用存储过程
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="spname">存储过程名</param>
        /// <param name="paras">存储过程参数</param>
        /// <param name="dicOutPar">带输出参数的值</param>
        /// <returns>返回结果集</returns>
        public bool ExecuteProcedure(
            string spname, IDictionary<string, object> paras,
            ref Dictionary<string, object> dicOutPar)
        {
            IDbConnection con = this.Database.Connection;
            con.Open();
            IDbCommand cmd = con.CreateCommand();
            cmd.CommandText = spname;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            if (paras != null)
            {
                foreach (string key in paras.Keys.Distinct())
                {
                    #region 添加参数 
                    IDbDataParameter pra = cmd.CreateParameter();
                    pra.ParameterName = key;
                    if (paras[key] is ParameterItem)
                    {
                        var pa = (ParameterItem)paras[key];
                        pra.Value = pa.Value;
                        pra.Direction = pa.Direction;
                        pra.Size = pa.Size;
                    }
                    else
                    {
                        pra.Value = paras[key];
                    }
                    #endregion

                    cmd.Parameters.Add(pra);
                }
            }
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                //执行存储过程
                cmd.ExecuteNonQuery();
                #region 获取参数返回的值
                foreach (IDbDataParameter dp in cmd.Parameters)
                {
                    if (dp.Direction == ParameterDirection.Output || dp.Direction == ParameterDirection.InputOutput ||
                        dp.Direction == ParameterDirection.ReturnValue)
                    {
                        dicOutPar.Add(dp.ParameterName, dp.Value);
                    }
                }
                #endregion
            }
            finally
            {
                con.Close();
            }
            return true;
        }



        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <typeparam name="T">返回结果集类型</typeparam> 
        /// <param name="spname">存储过程名称</param>
        /// <param name="paras">参数值</param>
        /// <returns></returns>
        public IList<T> CallProcedure<T>(
           string spname,
           IDictionary<string, object> paras = null)
        {
            var dicPar = new Dictionary<string, object>();
            return CallProcedure<T>(spname, ref dicPar, paras);
        }


        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <typeparam name="T">返回结果集的类型</typeparam>
        /// <param name="spname">存储过程名</param>
        /// <param name="dicOutPar">带输出参数的值</param>
        /// <param name="paras">存储过程参数</param>
        /// <returns>返回指定类型的结果集</returns>
        public IList<T> CallProcedure<T>(
            string spname,
            ref Dictionary<string, object> dicOutPar,
            IDictionary<string, object> paras = null)
        {
            Type type = typeof(T);

            IDbConnection con = this.Database.Connection;
            con.Open();
            IDbCommand cmd = con.CreateCommand();
            cmd.CommandTimeout = 90;
            cmd.CommandText = spname;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            if (paras != null)
            {
                foreach (string key in paras.Keys.Distinct())
                {
                    #region 添加参数
                    IDbDataParameter pra = cmd.CreateParameter();
                    pra.ParameterName = key;
                    if (paras[key] is ParameterItem)
                    {
                        ParameterItem pa = (ParameterItem)paras[key];
                        pra.Value = pa.Value;
                        pra.Direction = pa.Direction;
                        pra.Size = pa.Size;
                    }
                    else
                    {
                        pra.Value = paras[key];
                    }
                    #endregion
                    cmd.Parameters.Add(pra);
                }
            }

            IList<T> result = new List<T>();

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        #region 运用反射创建对象
                        object obj = type.Assembly.CreateInstance(type.Name);
                        ConstructorInfo constructure = type.GetConstructor(new Type[] { });
                        obj = constructure.Invoke(new Object[] { });
                        PropertyInfo[] pros = type.GetProperties();
                        string tempColumnName = string.Empty;
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            tempColumnName = dr.GetName(i);
                            var columnInfo = pros.Where(p => p.Name == tempColumnName);
                            if (columnInfo.Any())
                            {
                                Type tp = columnInfo.First().PropertyType;
                                object g = dr[tempColumnName];
                                if (!Convert.IsDBNull(dr[tempColumnName]))
                                {
                                    // 处理GUID
                                    if (tp.Name.ToLower() == "guid")
                                    {
                                        g = Guid.Parse(g.ToString());
                                    }
                                    // 处理Status属性Int 转换成String问题
                                    if (tempColumnName == "Status")
                                    {
                                        columnInfo.First().SetValue(obj, Convert.ChangeType(g, tp), new object[] { });
                                    }
                                    else if (g is long?)
                                    {
                                        columnInfo.First().SetValue(obj, new long?(Convert.ToInt64(g)), new object[] { });
                                    }
                                    else if (g is int?)
                                    {
                                        columnInfo.First().SetValue(obj, new int?(Convert.ToInt32(g)), new object[] { });
                                    }
                                    else if (g is DateTime?)
                                    {
                                        columnInfo.First().SetValue(obj, new DateTime?(Convert.ToDateTime(g)), new object[] { });
                                    }
                                    else
                                    {
                                        columnInfo.First().SetValue(obj, Convert.ChangeType(g, tp), new object[] { });
                                    }
                                }
                            }
                        }
                        #endregion
                        result.Add((T)obj);
                    }
                    dr.Close();
                    #region 获取参数返回的值
                    foreach (IDbDataParameter dp in cmd.Parameters)
                    {
                        if (dp.Direction == ParameterDirection.Output
                            || dp.Direction == ParameterDirection.InputOutput
                            || dp.Direction == ParameterDirection.ReturnValue)
                        {
                            dicOutPar.Add(dp.ParameterName, dp.Value);
                        }
                    }
                    #endregion
                    dr.Close();
                    cmd.Cancel();
                }
            }
            finally
            {
                con.Close();
            }
            return result;
        }




    }
}
