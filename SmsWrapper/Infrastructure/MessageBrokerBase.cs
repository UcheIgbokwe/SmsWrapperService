using System;
using Microsoft.Extensions.Logging;
using SmsWrapper.Model;

namespace SmsWrapper.Infrastructure
{
    public abstract class MessageBrokerBase : IDisposable
    {
        public abstract void ConnectToBroker();
        public abstract SmsEvent ConsumeMessageEvent(EventArgs Message);

        public abstract void Dispose();
    }
}