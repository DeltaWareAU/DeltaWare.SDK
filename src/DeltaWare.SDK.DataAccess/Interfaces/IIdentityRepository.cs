using System;
using System.Threading.Tasks;

namespace DeltaWare.SDK.DataAccess.Interfaces
{
    public interface IIdentityRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdentityAsync(Guid identity);
    }
}
