using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SmsWrapper.Infrastructure;
using SmsWrapper.Infrastructure.Interfaces;
using SmsWrapper.Model;

namespace SmsWrapper.Application
{
    public class SendSmsCommand : ISubscribe
    {
        private readonly ILogger<SendSmsCommand> _logger;
        private readonly IRepository _db;
        private readonly ISmsFactory _factory;
        private readonly IMessageBrokerClient _client;

        public SendSmsCommand(IRepository db)
        {
            _db = db;
        }
        public SendSmsCommand(ILogger<SendSmsCommand> logger, ISmsFactory factory, IMessageBrokerClient client)
        {
            _client = client;
            _factory = factory;
            _logger = logger;

        }

        public IRepository Repository 
        { 
            get
            {
                return _db;
            }

            set
            {

            }
        }

        /// <summary>
        /// This method handles the SMS lifecycle after message has been consumed by subscriber.
        /// </summary>
        public async Task HandleSms()
        {
            try
            {
                //Consume message from message queue.
                //Check if sms record already exist.
                //Verify the Sms provider using phone number.
                //Send Sms
                //Publish record.
                //Save record.
                
                var message = await _client.Subscribe();

                if (!await _db.SmsExist(message))
                {
                    var newMessage = message.CreateModel();
                    var config = _factory.CreateFactory(newMessage);
                    await config.PostSmsAsync(newMessage);
                    await _client.Publish(message);
                    await _db.AddSms(message);
                }
                else
                {
                    _logger.LogInformation("Message has already been sent.");
                    return;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in HandleSms: {ex.Message}");
                throw new Exception();
            }

        }

    }
}