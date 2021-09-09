using System.Threading.Tasks;
using SmsWrapper.Model;

namespace SmsWrapper.Infrastructure.Interfaces
{
    public interface IRepository
    {
        Task AddSms(SmsEvent smsEvent);
        Task<bool> SmsExist(SmsEvent smsEvent);
    }
}