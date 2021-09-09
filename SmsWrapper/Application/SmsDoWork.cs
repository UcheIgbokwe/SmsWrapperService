using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polly;
using SmsWrapper.Infrastructure.Interfaces;
using SmsWrapper.Model;

namespace SmsWrapper.Application
{
    public class SmsDoWork : ISmsClient
    {
        private readonly ISmsClient _client;
        private readonly ILogger<SmsDoWork> _logger;
        
        public SmsDoWork(ISmsClient client, ILogger<SmsDoWork> logger)
        {
            _logger = logger;
            _client = client;

        }
        public async Task<SmsResponse> PostSmsAsync(SmsEventDTO smsEventDTO)
        {
            try
            {
                var retryPolicy = Policy
                    .HandleResult<SmsResponse>(g => g.StatusCode != System.Net.HttpStatusCode.OK)
                    .RetryAsync(2, (exception, retryCount) => _logger.LogError($"Error in retryPolicy: {exception.Result}"));

                return await retryPolicy.ExecuteAsync(() => _client.PostSmsAsync(smsEventDTO));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in PostSmsAsync: {ex.Message}");
                throw new Exception();
            }
        }
    }
}