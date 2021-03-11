using DeltaWare.SDK.Web.Interfaces;
using System;

namespace DeltaWare.SDK.Web.Types
{
    public class ApiResponse<TResult>: IApiResponse<TResult>
    {
        public bool WasSuccessful { get; }

        public string Message { get; }

        public Exception Exception { get; }

        public TResult Result { get; }

        private ApiResponse(TResult result)
        {
            Result = result;

            WasSuccessful = true;
        }

        private ApiResponse(string message, Exception exception)
        {
            Message = message;

            Exception = exception;

            WasSuccessful = false;
        }

        public static IApiResponse<TResult> Success(TResult result) => new ApiResponse<TResult>(result);

        public static IApiResponse<TResult> Failure(string message, Exception exception = null) => new ApiResponse<TResult>(message, exception);
    }
}
