using Microsoft.Extensions.Logging.Abstractions;
using SmsWrapper.Application;
using Tests.SmsWrapper.Tests.Fake;
using Xunit;

namespace Tests.SmsWrapper.Tests
{
    public class SmsGatewayFactoryTest
    {
        [Fact]
        public void CheckIsSmsGatewayImplemented()
        {
            Assert.NotNull(SmsGatewayFactory.CreateGateway(new FakeRepository(),new FakeSmsFactory(),new FakeMessageBrokerClient(), new NullLoggerFactory()));
        }

        [Fact]
        public void CheckIsRepositorySetInSmsGateway()
        {
            var fakeDB = new FakeRepository();
            var gateway = SmsGatewayFactory.CreateGateway(fakeDB, new FakeSmsFactory(), new FakeMessageBrokerClient(), new NullLoggerFactory());
            Assert.NotNull(gateway);
            Assert.NotNull(gateway.Repository);
            Assert.Equal(fakeDB, gateway.Repository);
        }
    }
}