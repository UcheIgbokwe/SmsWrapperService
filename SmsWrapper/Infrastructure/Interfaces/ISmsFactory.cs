using SmsWrapper.Model;

namespace SmsWrapper.Infrastructure.Interfaces
{
    public interface ISmsFactory
    {
        ISmsClient CreateFactory(SmsEventDTO smsEventDTO);
    }
}