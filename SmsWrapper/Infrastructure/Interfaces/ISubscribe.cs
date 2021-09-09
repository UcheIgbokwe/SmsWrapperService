using System.Threading.Tasks;

namespace SmsWrapper.Infrastructure.Interfaces
{
    public interface ISubscribe
    {
        IRepository Repository { get; set; }
        Task HandleSms();
    }
}