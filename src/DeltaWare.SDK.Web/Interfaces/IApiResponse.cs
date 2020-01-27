namespace DeltaWare.SDK.Web.Interfaces
{
    public interface IApiResponse<out TEntity>
    {
        bool WasSuccessful { get; }

        TEntity Result { get; }
    }
}
