using System;

namespace DeltaWare.SDK.Core.Http
{
    public class FailureResponse
    {
        public string Message { get; set; }

        public string MessageDetail { get; set; }

        public FailureResponse()
        {
        }

        public FailureResponse(string message, string messageDetail = null)
        {
            this.Message = !string.IsNullOrWhiteSpace(message) ? message : throw new ArgumentNullException(nameof(message));
            this.MessageDetail = messageDetail;
        }
    }
}
