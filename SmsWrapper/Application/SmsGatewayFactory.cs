using System;
using Microsoft.Extensions.Logging;
using SmsWrapper.Infrastructure.Interfaces;

namespace SmsWrapper.Application
{
    public sealed class SmsGatewayFactory
    {
        public static ISubscribe CreateGateway(IRepository repository, ISmsFactory smsFactory, IMessageBrokerClient client, ILoggerFactory logger)
        {
            try
            {
                var _loggerFactory = logger.CreateLogger<SendSmsCommand>();
                ISubscribe smsGateway = new SendSmsCommand(repository, smsFactory, client, _loggerFactory);
                
                return smsGateway;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public static ISmsClient CreateClient(ISmsClient client, ILoggerFactory logger)
        {
            try
            {
                var _loggerFactory = logger.CreateLogger<SmsDoWork>();
                ISmsClient smsGateway = new SmsDoWork(client, _loggerFactory);
                return smsGateway;
            }
            catch (Exception)
            {
                throw new Exception();
            }
            
        }
    }
}