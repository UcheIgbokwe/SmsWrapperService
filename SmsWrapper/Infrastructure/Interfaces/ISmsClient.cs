using System.Threading.Tasks;
using SmsWrapper.Model;

namespace SmsWrapper.Infrastructure.Interfaces
{
    public interface ISmsClient
    {
        Task<SmsResponse> PostSmsAsync(SmsEventDTO smsEventDTO);
    }
}