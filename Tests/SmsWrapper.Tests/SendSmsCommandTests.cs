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
        /// Save and publish SMS when http post is successful.
        /// </summary>
        public void ShouldHandleSmsSaveWhenSuccessful()
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
        /// Sms record not saved or published when SMS http post fails all retries.
        /// </summary>
        public void ShouldHandleSmsNotSavedWhenRetryFails()
        {
            var Id = new Guid();
            var fakeDb = new FakeRepository();
            var gateway = SmsGatewayFactory.CreateGateway(fakeDb, new FakeSmsFactory(new FakeSmsClient(), new NullLoggerFactory()), new FakeMessageBrokerClient(), new NullLoggerFactory());
            Assert.NotNull(gateway);
            var test1 = new SmsEvent{
                DeliveryTag = Id,
                Message = "ref1",
                PhoneNumber = "ref2"
            };
            gateway.HandleSms(test1);

            Assert.Equal(0, fakeDb.Data.Count);
        }

        [Fact]
        /// <summary>
        /// Sms should be send duplicate record.
        /// </summary>
        public void ShouldHandleDuplicate()
        {
            var Id = new Guid();
            var fakeDb = new FakeRepository();
            var gateway = SmsGatewayFactory.CreateGateway(fakeDb, new FakeSmsFactory(new FakeSmsClient(HttpStatusCode.OK), new NullLoggerFactory()), new FakeMessageBrokerClient(), new NullLoggerFactory());
            Assert.NotNull(gateway);
            var test1 = new SmsEvent{
                DeliveryTag = Id,
                Message = "ref1",
                PhoneNumber = "ref2"
            };
            var test2 = new SmsEvent{
                DeliveryTag = Id,
                Message = "ref1",
                PhoneNumber = "ref2"
            };
            gateway.HandleSms(test1);
            gateway.HandleSms(test2);

            Assert.Equal(1, fakeDb.Data.Count);
        }

        [Fact]
        /// <summary>
        /// False acknowledgement is sent to.
        /// </summary>
        public void ShouldHandleAcknowlegmentWhenSmsFail()
        {
            var Id = new Guid();
            var fakeDb = new FakeRepository();
            var gateway = SmsGatewayFactory.CreateGateway(fakeDb, new FakeSmsFactory(new FakeSmsClient(), new NullLoggerFactory()), new FakeMessageBrokerClient(), new NullLoggerFactory());
            Assert.NotNull(gateway);
            var test1 = new SmsEvent{
                DeliveryTag = Id,
                Message = "ref1",
                PhoneNumber = "ref2"
            };
            gateway.HandleSms(test1);
            Assert.Equal(0, fakeDb.Data.Count);
        }
    }
}