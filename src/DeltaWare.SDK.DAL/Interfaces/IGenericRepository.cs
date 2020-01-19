using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeltaWare.SDK.DAL.Interfaces
{
    public interface IGenericRepository<TRepo> where TRepo : class
    {
        Task<TRepo> GetByIdAsync(long id);

        Task<TRepo> GetByIdentityAsync(Guid identity);

        Task InsertAsync(TRepo siteModel);

        void Update(TRepo siteModel);

        Task DeleteAsync(long id);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
