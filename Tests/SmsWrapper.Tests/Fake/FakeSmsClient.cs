using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using SmsWrapper.Infrastructure.Interfaces;
using SmsWrapper.Model;

namespace Tests.SmsWrapper.Tests.Fake
{
    public class FakeSmsClient : ISmsClient
    {
        private readonly HttpStatusCode _httpStatusCode;
        public FakeSmsClient()
        {
        }
        public FakeSmsClient(HttpStatusCode httpStatusCode)
        {
            _httpStatusCode = httpStatusCode;

        }
        public Queue<SmsResponse> _smsResponses = new();

        public Task<SmsResponse> PostSmsAsync(SmsEventDTO smsEventDTO)
        {
            if (!_smsResponses.TryDequeue(out var result))
            {
                result = new SmsResponse(_httpStatusCode);
            }
            return Task.FromResult(result);
        }
    }
}