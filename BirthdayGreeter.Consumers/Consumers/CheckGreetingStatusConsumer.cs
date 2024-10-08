using BirthdayGreeter.Consumers.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Contracts;

namespace BirthdayGreeter.Consumers.Consumers;

public class CheckGreetingStatusConsumer : IConsumer<CheckGreetingStatus>
{
    private readonly ILogger<CheckGreetingStatusConsumer> _logger;
    private readonly List<string> _greetingStatus = new List<string>
    { 
        "Read - thank you for your kind words", 
        "On the greetings read list - be patient",
        "Won't read - I hate birthdays",
    };  
    private string PickRandomString()
    {
        var rand = new Random();
        var randIndex = rand.Next(_greetingStatus.Count);
        return _greetingStatus[randIndex];
    }
    public CheckGreetingStatusConsumer(ILogger<CheckGreetingStatusConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CheckGreetingStatus> context)
    {
        // Use To introduce Timeout
        Thread.Sleep(0);
        await context.RespondAsync<GreetingStatusResult>(new GreetingStatusResult
        {
            GreetingId = context.Message.GreetingId,
            TimeStamp = DateTime.UtcNow,
            Status = PickRandomString()
        });
    }
}
