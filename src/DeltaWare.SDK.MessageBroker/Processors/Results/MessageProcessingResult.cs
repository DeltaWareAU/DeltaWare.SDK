using System;

namespace DeltaWare.SDK.MessageBroker.Processors.Results
{
    public class MessageProcessingResult : IMessageProcessingResult
    {
        public bool Retry { get; init; }

        public bool WasSuccessful { get; init; }

        public bool HasException => Exception != null;

        public Exception? Exception { get; init; }

        public string? Message { get; init; }

        public static IMessageProcessingResult Success(string? message = null) => new MessageProcessingResult
        {
            WasSuccessful = true,
            Message = message
        };

        public static IMessageProcessingResult Failure(string message, bool retry = false) => new MessageProcessingResult
        {
            Message = message,
            Retry = retry,
            WasSuccessful = false
        };

        public static IMessageProcessingResult Failure(Exception exception, bool retry = false) => new MessageProcessingResult
        {
            Exception = exception,
            Message = exception.Message,
            WasSuccessful = false,
            Retry = retry
        };

        public static IMessageProcessingResult Failure(Exception exception, string message, bool retry = false) => new MessageProcessingResult
        {
            Exception = exception,
            Message = message,
            WasSuccessful = false,
            Retry = retry
        };
    }
}
