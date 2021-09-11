using System;
using System.Net;
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

        public SendSmsCommand(IRepository db, ISmsFactory factory,  IMessageBrokerClient client, ILogger<SendSmsCommand> logger)
        {
            _db = db;
            _factory = factory;
            _client = client;
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
        public ISmsFactory SmsFactory 
        { 
            get
            {
                return _factory;
            }

            set
            {

            }
        }
        public IMessageBrokerClient Client
        {
            get
            {
                return _client;
            }

            set
            {

            }
        }

        public ILogger Logger 
        {
            get
            {
                return _logger;
            }

            set
            {

            }
        }

        /// <summary>
        /// This method consumes message from the queue.
        /// </summary>
        public async Task ConsumeSms()
        {
            try
            {
                var message = await _client.Consume();
                await HandleSms(message);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ConsumeSms: {ex.Message}");
                throw new Exception();
            }
        }

        /// <summary>
        /// This method handles the SMS lifecycle after message has been consumed by subscriber.
        /// </summary>
        public async Task HandleSms(SmsEvent message)
        {
            try
            {
                //Check if sms record already exist.
                //Verify the Sms provider using phone number.
                //Send Sms
                //Publish record.
                //Save record.
                if (!await _db.SmsExist(message))
                {
                    var newMessage = message.CreateModel();
                    var config = _factory.CreateFactory(newMessage);

                    var response = await config.PostSmsAsync(newMessage);
                    if(response.StatusCode == HttpStatusCode.OK)
                    {
                        await _client.Publish(message);
                        await _db.AddSms(message);
                        await _client.Acknowledge(message.DeliveryTag.ToString(), true);
                    }else{
                        //repetition logic
                        //save to failed db and retry again
                        //This is to ensure no text message is lost.
                        //The manual acknowledgement returns false.
                        await _client.Acknowledge(message.DeliveryTag.ToString(), false);
                        _logger.LogInformation("Message failed to send.");
                    }
                    
                    
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