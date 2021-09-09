using System.Threading.Tasks;
using SmsWrapper.Model;

namespace SmsWrapper.Infrastructure.Interfaces
{
    public interface IMessageBrokerClient
    {
        //Task<SmsEvent> Subscribe<T>(T message) where T : class;
        Task<SmsEvent> Subscribe();
        Task Publish<T>(T message) where T : class;
    }
}