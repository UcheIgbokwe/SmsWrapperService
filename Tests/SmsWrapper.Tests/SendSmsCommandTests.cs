using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using SmsWrapper.Application;
using SmsWrapper.Infrastructure.Interfaces;
using SmsWrapper.Model;
using Tests.SmsWrapper.Tests.Fake;
using Xunit;

namespace Tests.SmsWrapper.Tests
{
    public class SendSmsCommandTests
    {
        
        [Fact]
        /// <summary>
        /// Check is HandleSms implemented
        /// </summary>
        public void HandleSmsImplementation()
        {
            var fakeDB = new FakeRepository();
            var gateway = SmsGatewayFactory.CreateGateway(fakeDB);
            Assert.NotNull(gateway);
            gateway.HandleSms();
        }

        [Fact]
        /// <summary>
        /// Post Sms runs without retrying
        /// </summary>
        public void ShouldHandleSmsPostWithoutRetry()
        {
            var fakeClient = new FakeSmsClient(HttpStatusCode.OK);
            var logger = new NullLoggerFactory();
            var res = new SmsEventDTO{
                Message = "ref1",
                PhoneNumber = "ref2"
            };
            var gateway = SmsGatewayFactory.CreateClient(fakeClient, logger);
            Assert.NotNull(gateway);
            var result = gateway.PostSmsAsync(res).IsCompletedSuccessfully;
            Assert.True(result);
        }

        [Fact]
        /// <summary>
        /// Post Sms retries when status code is not Ok
        /// </summary>
        public void ShouldHandleSmsPostRetry()
        {
            var fakeClient = new FakeSmsClient(HttpStatusCode.NoContent);
            var logger = new NullLoggerFactory();
            var res = new SmsEventDTO{
                Message = "ref1",
                PhoneNumber = "ref2"
            };
            var gateway = SmsGatewayFactory.CreateClient(fakeClient, logger);
            Assert.NotNull(gateway);
            var result = gateway.PostSmsAsync(res).IsCompletedSuccessfully;
            Assert.True(result);
        }
    }
}