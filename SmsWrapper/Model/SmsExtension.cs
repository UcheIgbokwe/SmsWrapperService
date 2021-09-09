namespace SmsWrapper.Model
{
    public static class SmsExtension
    {
        public static SmsEventDTO CreateModel(this SmsEvent smsEvent)
        {
            return new SmsEventDTO
            {
                PhoneNumber = smsEvent.PhoneNumber,
                Message = smsEvent.Message
            };
        }
    }
}