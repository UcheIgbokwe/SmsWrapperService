using System;

namespace SmsWrapper.Model
{
    public class SmsEvent
    {
        public Guid MessageId { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}