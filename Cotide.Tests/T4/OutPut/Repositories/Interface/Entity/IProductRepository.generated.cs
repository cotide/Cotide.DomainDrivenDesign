using System;

using Cotide.Domain.Contracts.Repositories.Base;
using Cotide.Domain.Entity; 


namespace Cotide.Domain.Contracts.Repositories 
{
	/// <summary>
    ///   仓储操作层接口——Product
    /// </summary>
    public partial interface IProductRepository : IRepository<Product, Guid>
    {

    }
}
