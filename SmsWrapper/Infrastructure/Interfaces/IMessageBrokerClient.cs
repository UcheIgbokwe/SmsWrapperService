using System.Threading.Tasks;
using SmsWrapper.Model;

namespace SmsWrapper.Infrastructure.Interfaces
{
    public interface IMessageBrokerClient
    {
        Task<SmsEvent> Consume();
        Task Publish<T>(T message) where T : class;
    }
}