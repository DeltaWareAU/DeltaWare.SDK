﻿using System;

namespace DeltaWare.SDK.MessageBroker.Processors.Results
{
    public interface IMessageProcessingResult
    {
        public bool Retry { get; }

        public bool WasSuccessful { get; }

        public bool HasException { get; }

        public Exception? Exception { get; }

        public string? Message { get; }
    }
}
