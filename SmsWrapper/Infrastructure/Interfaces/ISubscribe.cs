using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SmsWrapper.Model;

namespace SmsWrapper.Infrastructure.Interfaces
{
    public interface ISubscribe
    {
        IRepository Repository { get; set; }
        ISmsFactory SmsFactory{ get; set; }
        IMessageBrokerClient Client { get; set; }
        ILogger Logger { get; set; }
        Task ConsumeSms();
        Task HandleSms(SmsEvent message);
    }
}