using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Primitives;
using Shared.Contracts;

namespace BirthdayGreeter.Producers.Api.Services;

public class BirthdayGreetingsService
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IBus _bus;
    private readonly IRequestClient<CheckGreetingStatus> _requestClient;
    private readonly IMediator _mediator;
    public BirthdayGreetingsService(IPublishEndpoint publishEndpoint, IBus bus, IRequestClient<CheckGreetingStatus> requestClient, IMediator mediator) 
    {
        _publishEndpoint = publishEndpoint;
        _bus = bus;
        _requestClient = requestClient;
        _mediator = mediator;
    }

    /// <summary>
    /// Your very first message to the bus!
    /// 
    /// 
    /// IBus should only be used by an initiator
    /// a process that is initiating a business proces
    /// </summary>
    /// <returns></returns>
    public async Task SendMessage(VerySpecialGreeting greeting)
    {
        // Using IBus send to certain endpoint
        var endpoint = await _bus.GetSendEndpoint(new Uri("exchange:very-special-greeting"));
        await endpoint.Send(greeting);
    }


    /// <summary>
    /// Publish to all consumers that would like this message
    /// </summary>
    /// <param name="greeting"></param>
    /// <returns></returns>
    public async Task PublishMessage(Greeting greeting)
    {
        await _publishEndpoint.Publish(greeting);
    }

    /// <summary>
    /// Publish message with bad language to demonstrate the exception features(dead-letter queue)
    /// </summary>
    /// <param name="greeting"></param>
    /// <returns></returns>
    public async Task PublishMessageWithBadLanguage(Greeting greeting)
    {
        greeting.Text += " You suck";

        await _publishEndpoint.Publish(greeting);
    }

    public async Task<GreetingStatusResult> RequestResponseStatusMessage(string greetingId)
    {
        // Use Timeout
        var timeout = TimeSpan.FromSeconds(5);
        using var source = new CancellationTokenSource(timeout);
        // Request status
        var res =  await _requestClient.GetResponse<GreetingStatusResult>(new { greetingId }, source.Token);
        return res.Message;
    }

    public async Task<string> MediatorFireAndForgetMessage(VeryLargeAndComplexGreeting greeting)
    {
        await _mediator.Publish(greeting);
        return "Let Me Handle This VeryLargeAndComplexGreeting, You go on with your day"; 
    }
}
