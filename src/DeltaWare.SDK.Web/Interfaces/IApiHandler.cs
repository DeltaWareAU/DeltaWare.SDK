using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Web.Interfaces
{
    public interface IApiHandler<TEntity>
    {
        Task<IApiResponse<IEnumerable<TEntity>>> GetAsync(IApiVersion version);

        Task<IApiResponse<TEntity>> GetAsync(IApiVersion version, Guid identity);

        Task<IApiResponse<TEntity>> CreateAsync(IApiVersion version, TEntity entity);

        Task<IApiResponse<TEntity>> DeleteAsync(IApiVersion version, Guid identity);

        Task<IApiResponse<TEntity>> UpdateAsync(IApiVersion version, TEntity entity);
    }
}
