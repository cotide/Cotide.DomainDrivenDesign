using System;

using Cotide.Domain.Contracts.Repositories.Base;
using Cotide.Domain.Entity; 


namespace Cotide.Domain.Contracts.Repositories 
{
	/// <summary>
    ///   仓储操作层接口——ShopFavorite
    /// </summary>
    public partial interface IShopFavoriteRepository : IRepository<ShopFavorite, Guid>
    {

    }
}
