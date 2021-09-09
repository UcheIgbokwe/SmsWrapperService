# Sms-Wrapper Service
This project is a clean architecture implementation of modelling an event-driven microservice using .NET 5.

## Resources and Libraries

- .NET 5
- Polly
- Xunit

## Introduction
The Service consumes SMS request to be sent to customers from a message queue. The request is then posted via HTTP to
a valid SMS sending service, records of the sent sms is saved to an event bus.

The " Send Sms" process has the following stages:
1. HandleSms – This houses the various abstract methods for a complete process E2E. 
2. Check if record exist – The `SmsExist` method checks if the sms record already exist/sent.
3. Extract the SmsEvent from the `Subscribe` method. This is an abstraction of connection to the message broker and creating the
necessary Client object which includes routing key, queue, exchange details.
4. Next is the `PostSmsAsync` method. It has a retry policy to post to the sms to the provided sms service.
5. The successfully sent sms is saved and published.

To run the service, cd into the Project folder and run the command below:

```bash
dotnet test
```  
