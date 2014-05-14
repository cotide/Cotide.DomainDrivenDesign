 
using System;
using System.ComponentModel.Composition;
using System.Linq;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Entity;
using Cotide.Infrastructure.Repositories.Base; 


namespace Cotide.Infrastructure.Repositories
{
	/// <summary>
    ///   仓储操作层实现——ShopFavorite
    /// </summary> 
    public partial class ShopFavoriteRepository : EFRepositoryBase<ShopFavorite, Guid>, IShopFavoriteRepository
    { 
	}
}
