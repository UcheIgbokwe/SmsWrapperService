using System;
using Microsoft.Extensions.Logging;
using SmsWrapper.Infrastructure.Interfaces;
using SmsWrapper.Model;

namespace SmsWrapper.Application
{
    public class SmsFactory : ISmsFactory
    {
        private readonly Func<ISmsClient> _client;
        private readonly ILoggerFactory _loggerFactory;
        public SmsFactory(Func<ISmsClient> client, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _client = client;

        }
        public ISmsClient CreateFactory(SmsEventDTO smsEventDTO)
        {
            var client = _client();
            var logger = _loggerFactory.CreateLogger<SmsDoWork>();
            return new SmsDoWork(client, logger);
        }
    }
}