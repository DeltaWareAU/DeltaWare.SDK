using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Web.Interfaces
{
    public interface IApiHandler<TEntity, in TVersion> where TEntity : class where TVersion : IApiVersion
    {
        Task<IApiResponse<IEnumerable<TEntity>>> GetAsync(TVersion version);

        Task<IApiResponse<TEntity>> GetAsync(TVersion version, Guid identity);

        Task<IApiResponse<TEntity>> CreateAsync(TVersion version, TEntity entity);

        Task<IApiResponse<TEntity>> DeleteAsync(TVersion version, Guid identity);

        Task<IApiResponse<TEntity>> UpdateAsync(TVersion version, TEntity entity);
    }
}
