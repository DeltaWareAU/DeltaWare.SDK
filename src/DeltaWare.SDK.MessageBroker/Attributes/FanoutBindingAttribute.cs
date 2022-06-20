﻿using DeltaWare.SDK.MessageBroker.Messages.Enums;

namespace DeltaWare.SDK.MessageBroker.Attributes
{
    public class FanoutBindingAttribute : MessageBrokerBindingAttribute
    {
        public FanoutBindingAttribute(string name) : base(name, BrokerExchangeType.Fanout)
        {
        }
    }
}