using MassTransit;
using Shared.Contracts;

namespace BirthdayGreeter.Producers.Api.Consumers;

public class MediatorConsumer : IConsumer<VeryLargeAndComplexGreeting>
{
    public async Task Consume(ConsumeContext<VeryLargeAndComplexGreeting> context)
    {
        var message = context.Message;
        Console.WriteLine($"Greeting received: {message}");

        // Simulate work asynchronously in the background
        _ = Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(20));

            Console.WriteLine($"Greeting {message} processed in background");
        });
        await Task.CompletedTask;
    }
}
