using BirthdayGreeter.Consumers.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Shared.Contracts;

namespace BirthdayGreeter.Consumers.Consumers;

public class GreetingsSendConsumer : IConsumer<VerySpecialGreeting>
{
    private readonly IHubContext<BirthdayHub> _hubcontext;
    private readonly ILogger<GreetingsSendConsumer> _logger;
    public GreetingsSendConsumer(ILogger<GreetingsSendConsumer> logger, IHubContext<BirthdayHub> hubcontext) 
    {
        _logger = logger;
        _hubcontext = hubcontext;
    }
    public async Task Consume(ConsumeContext<VerySpecialGreeting> context)
    {
        await _hubcontext.Clients.All.SendAsync("ReceiveBirthdayNotification", context.Message.ToDTO("GreetingsSendConsumer"));        Console.WriteLine(context.Message.ToStringWithConsumer("GreetingsSendConsumer"));
        await Task.CompletedTask;
    }
}
