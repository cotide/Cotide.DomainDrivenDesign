using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Cotide.Framework.GC;

namespace Cotide.Domain.Contracts.Repositories.Base
{
    /// <summary>
    /// Represents 抽象基类
    /// </summary>
    public abstract class RepositoryContext : DisposableObject, IRepositoryContext
    {
        #region Private Fields
        private readonly Guid id = Guid.NewGuid();
        private readonly ThreadLocal<List<object>> _localNewCollection = new ThreadLocal<List<object>>(() => new List<object>());
        private readonly ThreadLocal<List<object>> _localModifiedCollection = new ThreadLocal<List<object>>(() => new List<object>());
        private readonly ThreadLocal<List<object>> _localDeletedCollection = new ThreadLocal<List<object>>(() => new List<object>());
        private readonly ThreadLocal<bool> _localCommitted = new ThreadLocal<bool>(() => true);
        #endregion
 
        /// <summary>
        /// 获取新的仓储集合
        /// </summary>
        protected IEnumerable<object> NewCollection
        {
            get { return _localNewCollection.Value; }
        }
        /// <summary>
        /// 获取修改的仓储集合
        /// </summary>
        protected IEnumerable<object> ModifiedCollection
        {
            get { return _localModifiedCollection.Value; }
        }
        /// <summary>
        /// 获取删除的仓储集合
        /// </summary>
        protected IEnumerable<object> DeletedCollection
        {
            get { return _localDeletedCollection.Value; }
        }
        
        #region Protected Methods
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <remarks>初始化仓储</remarks>
        protected void ClearRegistrations()
        {
            this._localNewCollection.Value.Clear();
            this._localModifiedCollection.Value.Clear();
            this._localDeletedCollection.Value.Clear();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing">A <see cref="System.Boolean"/> value which indicates whether
        /// the object should be disposed explicitly.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._localCommitted.Dispose();
                this._localDeletedCollection.Dispose();
                this._localModifiedCollection.Dispose();
                this._localNewCollection.Dispose();
            }
        }
        
        /// <summary>
        /// 获取当前仓储的Domain Id
        /// </summary>
        public Guid ID
        {
            get { return id; }
        }
        /// <summary>
        /// 注册新的仓储
        /// </summary>
        /// <param name="obj">The object to be registered.</param>
        public virtual void RegisterNew(object obj)
        { 
            _localNewCollection.Value.Add(obj);
            Committed = false;
        }
        /// <summary>
        /// 注册修改的仓储
        /// </summary>
        /// <param name="obj">The object to be registered.</param>
        public virtual void RegisterModified(object obj)
        {
            if (_localDeletedCollection.Value.Contains(obj))
                throw new InvalidOperationException("The object cannot be registered as a modified object since it was marked as deleted.");
            if (!_localModifiedCollection.Value.Contains(obj) && !_localNewCollection.Value.Contains(obj))
                _localModifiedCollection.Value.Add(obj);
            Committed = false;
        }
        /// <summary>
        /// 注册删除的仓储
        /// </summary>
        /// <param name="obj">The object to be registered.</param>
        public virtual void RegisterDeleted(object obj)
        {
            if (_localNewCollection.Value.Contains(obj))
            {
                if (_localNewCollection.Value.Remove(obj))
                    return;
            }
            bool removedFromModified = _localModifiedCollection.Value.Remove(obj);
            bool addedToDeleted = false;
            if (!_localDeletedCollection.Value.Contains(obj))
            {
                _localDeletedCollection.Value.Add(obj);
                addedToDeleted = true;
            }
            _localCommitted.Value = !(removedFromModified || addedToDeleted);
        }
        #endregion

        #region IUnitOfWork Members
        /// <summary>
        /// Gets a <see cref="System.Boolean"/> value which indicates
        /// whether the Unit of Work could support Microsoft Distributed
        /// Transaction Coordinator (MS-DTC).
        /// </summary>
        public virtual bool DistributedTransactionSupported
        {
            get { return false; }
        }
        /// <summary>
        /// 获取 <see cref="System.Boolean"/> 值
        /// 查看是否成功提交.
        /// </summary>
        public virtual bool Committed
        {
            get { return _localCommitted.Value; }
            protected set { _localCommitted.Value = value; }
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public abstract void Commit();

        /// <summary>
        /// 回滚事务
        /// </summary>
        public abstract void Rollback();

        #endregion
    }
}
