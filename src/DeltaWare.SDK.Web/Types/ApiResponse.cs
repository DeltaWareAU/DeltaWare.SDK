using DeltaWare.SDK.Web.Interfaces;

namespace DeltaWare.SDK.Web.Types
{
    public class ApiResponse<TResult> : IApiResponse<TResult>
    {
        public bool WasSuccessful { get; }

        public TResult Result { get; }

        private ApiResponse(TResult result)
        {
            Result = result;

            WasSuccessful = true;
        }

        private ApiResponse()
        {
            Result = default;

            WasSuccessful = false;
        }

        public static IApiResponse<TResult> Success(TResult result) => new ApiResponse<TResult>(result);

        public static IApiResponse<TResult> Failure() => new ApiResponse<TResult>();
    }
}
