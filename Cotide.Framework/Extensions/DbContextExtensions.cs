using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Cotide.Framework.Domain;
using Cotide.Framework.EF;

namespace Cotide.Framework.Extensions
{
    public static class DbContextExtensions
    {
         
        public static IQueryable<TEntity> FindAll<TEntity, T1>(
            this DbContext dbContext) where TEntity : EntityByType<T1>  
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext"); 
            return  dbContext.Set<TEntity>(); 
        }


        public static IQueryable<TEntity> FindAll<TEntity>(
            this DbContext dbContext) where TEntity : EntityByType<long>
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            return dbContext.Set<TEntity>();
        }
 

        public static TEntity FindOne<TEntity,T1>(this DbContext dbContext,
            Expression<Func<TEntity,bool>> predicate) where TEntity : EntityByType<T1>
        { 
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            return dbContext.Set<TEntity>().Where(predicate).FirstOrDefault(); 
        }

         
        public static TEntity FindOne<TEntity>(
            this DbContext dbContext,
            long id) where TEntity : EntityByType<long>
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            return dbContext.Set<TEntity>().FirstOrDefault(x => x.Id == id);
        }

         
        public static TEntity FindOne<TEntity>(
            this DbContext dbContext,
            string id) where TEntity : EntityByType<string>
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            return dbContext.Set<TEntity>().FirstOrDefault(x => x.Id == id);
        }


         

         
        public static void Add<TEntity, T1>(
            this DbContext dbContext,
            params TEntity[] entities) where TEntity : EntityByType<T1>
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            if (entities == null) throw new ArgumentNullException("entities");

            foreach (TEntity entity in entities)
            {
                DbSet<TEntity> dbSet = dbContext.Set<TEntity>();
                try
                {
                    DbEntityEntry<TEntity> entry = dbContext.Entry(entity);
                    if (entry.State == EntityState.Detached)
                    {
                        dbSet.Attach(entity);
                        entry.State = EntityState.Added;
                    }
                }
                catch (InvalidOperationException)
                {
                    TEntity oldEntity = dbSet.Find(entity.Id);
                    dbContext.Entry(oldEntity).CurrentValues.SetValues(entity);
                }
            }
        }

     


        public static void Update<TEntity, T1>(
            this DbContext dbContext, 
            params TEntity[] entities) where TEntity : EntityByType<T1>  
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            if (entities == null) throw new ArgumentNullException("entities");

            foreach (TEntity entity in entities)
            {
                DbSet<TEntity> dbSet = dbContext.Set<TEntity>();
                try
                {
                    DbEntityEntry<TEntity> entry = dbContext.Entry(entity);
                    if (entry.State == EntityState.Detached)
                    {
                        dbSet.Attach(entity);
                        entry.State = EntityState.Modified;
                    }
                }
                catch (InvalidOperationException)
                {
                    TEntity oldEntity = dbSet.Find(entity.Id);
                    dbContext.Entry(oldEntity).CurrentValues.SetValues(entity);
                }
            }
        }

        public static void Update<TEntity, T1>(
            this DbContext dbContext,
            Expression<Func<TEntity, object>> propertyExpression, 
            params TEntity[] entities)
            where TEntity : EntityByType<T1>
        {
            if (propertyExpression == null) throw new ArgumentNullException("propertyExpression");
            if (entities == null) throw new ArgumentNullException("entities");
            ReadOnlyCollection<MemberInfo> memberInfos = ((dynamic)propertyExpression.Body).Members;
            foreach (TEntity entity in entities)
            {
                DbSet<TEntity> dbSet = dbContext.Set<TEntity>();
                try
                {
                    DbEntityEntry<TEntity> entry = dbContext.Entry(entity);
                    entry.State = EntityState.Unchanged;
                    foreach (var memberInfo in memberInfos)
                    {
                        entry.Property(memberInfo.Name).IsModified = true;
                    }
                }
                catch (InvalidOperationException)
                {
                    TEntity originalEntity = dbSet.Local.Single(m => Equals(m.Id, entity.Id));
                    ObjectContext objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;
                    ObjectStateEntry objectEntry = objectContext.ObjectStateManager.GetObjectStateEntry(originalEntity);
                    objectEntry.ApplyCurrentValues(entity);
                    objectEntry.ChangeState(EntityState.Unchanged);
                    foreach (var memberInfo in memberInfos)
                    {
                        objectEntry.SetModifiedProperty(memberInfo.Name);
                    }
                }
            }
        }


        /// <summary>
        /// 转换成ObjectContext
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns> 
        public static ObjectContext ToExcuteSqlDbContext(this DbContext dbContext)
        {

            return ((IObjectContextAdapter)dbContext).ObjectContext;
        }

        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="validateOnSaveEnabled"></param>
        /// <returns></returns>
        public static int SaveChanges(
            this DbContext dbContext, 
            bool validateOnSaveEnabled)
        {
            bool isReturn = dbContext.Configuration.ValidateOnSaveEnabled != validateOnSaveEnabled;
            try
            {
                dbContext.Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
                return dbContext.SaveChanges();
            }
            finally
            {
                if (isReturn)
                {
                    dbContext.Configuration.ValidateOnSaveEnabled = !validateOnSaveEnabled;
                }
            }
        }

        /// <summary>
        /// 通过Castle调用存储过程
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="spname">存储过程名</param>
        /// <param name="paras">存储过程参数</param>
        /// <returns>返回结果集</returns>
        public static bool ExecuteProcedure(
            this DbContext dbContext,
            string spname, 
            IDictionary<string, object> paras)
        {
            var dicOutPar = new Dictionary<string, object>();
            return ExecuteProcedure(dbContext,spname, paras, ref dicOutPar);
        }


        /// <summary>
        /// 通过Castle调用存储过程
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="spname">存储过程名</param>
        /// <param name="paras">存储过程参数</param>
        /// <param name="dicOutPar">带输出参数的值</param>
        /// <returns>返回结果集</returns>
        public static bool ExecuteProcedure(
            this DbContext dbContext, 
            string spname, IDictionary<string, object> paras,
            ref Dictionary<string, object> dicOutPar)
        {
            IDbConnection con = dbContext.Database.Connection;
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
        /// <typeparam name="T"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="spname"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static IList<T> CallProcedure<T>(
           this DbContext dbContext, 
           string spname,
           IDictionary<string, object> paras = null)
        {
            var dicPar = new Dictionary<string, object>();
            return CallProcedure<T>(dbContext,spname, ref dicPar, paras);
        }


        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <typeparam name="T">返回结果集的类型</typeparam>
        /// <param name="dbContext"></param>
        /// <param name="spname">存储过程名</param>
        /// <param name="paras">存储过程参数</param>
        /// <param name="dicOutPar">带输出参数的值</param>
        /// <returns>返回指定类型的结果集</returns>
        public static IList<T> CallProcedure<T>(
            this DbContext dbContext, 
            string spname,
            ref Dictionary<string, object> dicOutPar,
            IDictionary<string, object> paras = null)
        {
            Type type = typeof(T);

            IDbConnection con = dbContext.Database.Connection;
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
