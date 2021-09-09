using System.Net;

namespace SmsWrapper.Model
{
    public class SmsResponse
    {
        public SmsResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
        public HttpStatusCode StatusCode { get; set; }
    }
}