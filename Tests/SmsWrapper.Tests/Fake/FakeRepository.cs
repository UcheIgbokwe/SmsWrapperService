using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmsWrapper.Infrastructure.Interfaces;
using SmsWrapper.Model;

namespace Tests.SmsWrapper.Tests.Fake
{
    public class FakeRepository : IRepository
    {
        public IList<SmsEvent> Data = new List<SmsEvent>();

        public Task AddSms(SmsEvent smsEvent)
        {
            Data.Add(smsEvent);
            return Task.CompletedTask;
        }
        public Task<bool> SmsExist(SmsEvent smsEvent)
        {
            return Task.FromResult(Data.Any(w => w.MessageId != smsEvent.MessageId));
        }
    }
}