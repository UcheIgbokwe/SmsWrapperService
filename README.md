# Sms-Wrapper Service
This project is a clean architecture implementation of modelling an event-driven microservice using .NET 5.

## Resources and Libraries

- .NET 5
- Polly
- Xunit

## Introduction
The Service consumes SMS request to be sent to customers from a message queue. The request is then posted via HTTP to
a valid SMS sending service, records of the sent sms is saved and also published to an event bus.

The " Send Sms Command" process has the following stages:
1. ConsumeSms - Connects to the message queue and extracts the message to be sent.
2. HandleSms – This recieves the extracted message and completes the process of sending and publishing. 
3. Consume event from message queue using `Consume` method. This is an abstraction of connection to the message broker and creating the
necessary Client object which includes routing key, queue, exchange details.
4. Check if record exist – The `SmsExist` method checks if the sms record already exist/sent.
5. Next is the `PostSmsAsync` method. It has a retry policy to post to the sms to the provided sms service.
6. If `PostSmsAsync` returns a Status Ok, the sms record is published and saved.

To run the service, cd into the Project folder and run the command below:

```bash
dotnet test
```  
