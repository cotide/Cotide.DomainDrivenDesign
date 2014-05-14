using System; 
using Cotide.Domain.Contracts.Repositories.Base;
using Cotide.Domain.Entity;  
namespace Cotide.Domain.Contracts.Repositories 
{
	/// <summary>
    ///   仓储操作层接口——Client
    /// </summary>
    public partial interface IClientRepository : IRepository<Client, Guid>
    {

    }
}
