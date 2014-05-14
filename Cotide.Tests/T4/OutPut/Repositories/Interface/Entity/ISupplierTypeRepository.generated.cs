using System;

using Cotide.Domain.Contracts.Repositories.Base;
using Cotide.Domain.Entity; 


namespace Cotide.Domain.Contracts.Repositories 
{
	/// <summary>
    ///   仓储操作层接口——SupplierType
    /// </summary>
    public partial interface ISupplierTypeRepository : IRepository<SupplierType, Guid>
    {

    }
}
