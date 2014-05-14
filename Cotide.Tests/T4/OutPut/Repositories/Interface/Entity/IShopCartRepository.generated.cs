using System;

using Cotide.Domain.Contracts.Repositories.Base;
using Cotide.Domain.Entity; 


namespace Cotide.Domain.Contracts.Repositories 
{
	/// <summary>
    ///   仓储操作层接口——ShopCart
    /// </summary>
    public partial interface IShopCartRepository : IRepository<ShopCart, Int32>
    {

    }
}
