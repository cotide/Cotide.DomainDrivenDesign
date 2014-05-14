 
using System;
using System.ComponentModel.Composition;
using System.Linq;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Entity;
using Cotide.Infrastructure.Repositories.Base; 


namespace Cotide.Infrastructure.Repositories
{
	/// <summary>
    ///   仓储操作层实现——ShopCart
    /// </summary> 
    public partial class ShopCartRepository : EFRepositoryBase<ShopCart, Int32>, IShopCartRepository
    { 
	}
}
