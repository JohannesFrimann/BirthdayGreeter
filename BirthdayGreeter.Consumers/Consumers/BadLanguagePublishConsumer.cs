using BirthdayGreeter.Consumers.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Shared.Contracts;

public class BadLanguagePublishConsumer : IConsumer<Greeting>
{
    private readonly IHubContext<BirthdayHub> _hubcontext;
    private readonly ILogger<BadLanguagePublishConsumer> _logger;
    public BadLanguagePublishConsumer(ILogger<BadLanguagePublishConsumer> logger, IHubContext<BirthdayHub> hubContext)
    {
        _logger = logger;
        _hubcontext = hubContext;
    }
    public async Task Consume(ConsumeContext<Greeting> context)
    {
        //if(context.Message.Text.Contains("You suck")){
        //    await _hubcontext.Clients.All.SendAsync("ReceiveBirthdayNotification", "Threw an error cause of bad language");
        //    throw new DontSayPeopleSuckOnTheirBirthdayException("This is not nice!");
        //}

        
        // Demonstrate moving from error queue
        if(context.Message.Text.Contains("You suck"))
        {
            // Lets mask out these bad words
            context.Message.Text = context.Message.Text.Replace("You suck", "You're the greatest person alive");
        }

        await _hubcontext.Clients.All.SendAsync("ReceiveBirthdayNotification", context.Message.ToDTO("BadLanguagePublishConsumer"));
        Console.WriteLine(context.Message.ToStringWithConsumer("BadLanguagePublishConsumer"));
        await Task.CompletedTask;
    }
}