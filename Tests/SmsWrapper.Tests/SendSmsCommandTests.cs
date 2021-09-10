using System;
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
            var gateway = SmsGatewayFactory.CreateGateway(new FakeRepository(), new FakeSmsFactory(), new FakeMessageBrokerClient(), new NullLoggerFactory());
            Assert.NotNull(gateway);
            gateway.HandleSms(new SmsEvent());
        }

        [Fact]
        /// <summary>
        /// Check is ConsumeSms implemented
        /// </summary>
        public void ConsumeSmsImplementation()
        {
            var gateway = SmsGatewayFactory.CreateGateway(new FakeRepository(), new FakeSmsFactory(), new FakeMessageBrokerClient(), new NullLoggerFactory());
            Assert.NotNull(gateway);
            gateway.ConsumeSms();
        }

        [Fact]
        /// <summary>
        /// Post Sms runs without retrying
        /// </summary>
        public void ShouldHandleSmsPostWithoutRetry()
        {
            var res = new SmsEventDTO{
                Message = "ref1",
                PhoneNumber = "ref2"
            };
            var gateway = SmsGatewayFactory.CreateClient(new FakeSmsClient(), new NullLoggerFactory());
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
            var res = new SmsEventDTO{
                Message = "ref1",
                PhoneNumber = "ref2"
            };
            var gateway = SmsGatewayFactory.CreateClient(new FakeSmsClient(), new NullLoggerFactory());
            Assert.NotNull(gateway);
            var result = gateway.PostSmsAsync(res).IsCompletedSuccessfully;
            Assert.True(result);
        }

        [Fact]
        /// <summary>
        /// Sms should be send duplicate record.
        /// </summary>
        public void ShouldHandleDuplicate()
        {
            var Id = new Guid();
            var fakeDb = new FakeRepository();
            var gateway = SmsGatewayFactory.CreateGateway(fakeDb, new FakeSmsFactory(new FakeSmsClient(), new NullLoggerFactory()), new FakeMessageBrokerClient(), new NullLoggerFactory());
            Assert.NotNull(gateway);
            var test1 = new SmsEvent{
                MessageId = Id,
                Message = "ref1",
                PhoneNumber = "ref2"
            };
            var test2 = new SmsEvent{
                MessageId = Id,
                Message = "ref1",
                PhoneNumber = "ref2"
            };
            gateway.HandleSms(test1);
            gateway.HandleSms(test2);

            Assert.Equal(1, fakeDb.Data.Count);
        }
    }
}