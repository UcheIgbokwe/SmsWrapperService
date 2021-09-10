using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SmsWrapper.Application;
using SmsWrapper.Infrastructure.Interfaces;
using SmsWrapper.Model;

namespace Tests.SmsWrapper.Tests.Fake
{
    public class FakeSmsFactory : ISmsFactory
    {
        private readonly NullLoggerFactory _loggerFactory;
        private readonly FakeSmsClient _fakeSmsClient;
        public FakeSmsFactory()
        {
        }
        public FakeSmsFactory(FakeSmsClient fakeSmsClient, NullLoggerFactory loggerFactory)
        {
            _fakeSmsClient = fakeSmsClient;
            _loggerFactory = loggerFactory;

        }
        public ISmsClient CreateFactory(SmsEventDTO smsEventDTO)
        {
            var logger = _loggerFactory.CreateLogger<SmsDoWork>();
            ISmsClient smsGateway = new SmsDoWork(_fakeSmsClient, logger);
            return smsGateway;
        }
    }
}