using System;

namespace DeltaWare.SDK.Web.Interfaces
{
    public interface IApiResponse<out TEntity>
    {
        bool WasSuccessful { get; }

        string Message { get; }

        Exception Exception { get; }

        TEntity Result { get; }
    }
}
