using BirthdayGreeter.Consumers.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Shared.Contracts;

namespace BirthdayGreeter.Consumers.Consumers;

public class GreetingsPublishConsumer : IConsumer<Greeting>
{
    private readonly IHubContext<BirthdayHub> _hubcontext;
    private readonly ILogger<GreetingsPublishConsumer> _logger;
    public GreetingsPublishConsumer(ILogger<GreetingsPublishConsumer> logger, IHubContext<BirthdayHub> hubcontext)
    {
        _logger = logger;
        _hubcontext = hubcontext;
    }
    public async Task Consume(ConsumeContext<Greeting> context)
    {
        await _hubcontext.Clients.All.SendAsync("ReceiveBirthdayNotification", context.Message.ToDTO("GreetingsPublishConsumer"));
        Console.WriteLine(context.Message.ToStringWithConsumer("GreetingsPublishConsumer"));
        await Task.CompletedTask;
    }
}

public class GreetingsPublishConsumer1 : IConsumer<Greeting>
{
    private readonly IHubContext<BirthdayHub> _hubcontext;
    private readonly ILogger<GreetingsPublishConsumer1> _logger;
    public GreetingsPublishConsumer1(ILogger<GreetingsPublishConsumer1> logger, IHubContext<BirthdayHub> hubcontext)
    {
        _logger = logger;
        _hubcontext = hubcontext;
    }
    public async Task Consume(ConsumeContext<Greeting> context)
    {
        await _hubcontext.Clients.All.SendAsync("ReceiveBirthdayNotification", context.Message.ToDTO("GreetingsPublishConsumer1"));

        Console.WriteLine(context.Message.ToStringWithConsumer("GreetingsPublishConsumer1"));
        await Task.CompletedTask;
    }
}
public class GreetingsPublishConsumer2 : IConsumer<Greeting>
{
    private readonly IHubContext<BirthdayHub> _hubcontext;
    private readonly ILogger<GreetingsPublishConsumer2> _logger;
    public GreetingsPublishConsumer2(ILogger<GreetingsPublishConsumer2> logger, IHubContext<BirthdayHub> hubcontext)
    {
        _logger = logger;
        _hubcontext = hubcontext;
    }
    public async Task Consume(ConsumeContext<Greeting> context)
    {
        await _hubcontext.Clients.All.SendAsync("ReceiveBirthdayNotification", context.Message.ToDTO("GreetingsPublishConsumer2"));

        Console.WriteLine(context.Message.ToStringWithConsumer("GreetingsPublishConsumer2"));
        await Task.CompletedTask;
    }
}
public class GreetingsPublishConsumer3 : IConsumer<Greeting>
{
    private readonly IHubContext<BirthdayHub> _hubcontext;
    private readonly ILogger<GreetingsPublishConsumer3> _logger;
    public GreetingsPublishConsumer3(ILogger<GreetingsPublishConsumer3> logger, IHubContext<BirthdayHub> hubcontext)
    {
        _logger = logger;
        _hubcontext = hubcontext;
    }
    public async Task Consume(ConsumeContext<Greeting> context)
    {
        await _hubcontext.Clients.All.SendAsync("ReceiveBirthdayNotification", context.Message.ToDTO("GreetingsPublishConsumer3"));

        Console.WriteLine(context.Message.ToStringWithConsumer("GreetingsPublishConsumer3"));
        await Task.CompletedTask;
    }
}