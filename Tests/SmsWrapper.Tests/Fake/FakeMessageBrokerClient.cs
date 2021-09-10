using System.Threading.Tasks;
using SmsWrapper.Infrastructure.Interfaces;
using SmsWrapper.Model;

namespace Tests.SmsWrapper.Tests.Fake
{
    public class FakeMessageBrokerClient : IMessageBrokerClient
    {
        public Task Publish<T>(T message) where T : class
        {
            return Task.CompletedTask;
        }

        public Task<SmsEvent> Consume()
        {
            return Task.FromResult(new SmsEvent());
        }
    }
}