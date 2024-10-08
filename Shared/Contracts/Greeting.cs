namespace Shared.Contracts;

public class Greeting
{
    public string Sender { get; set; } = null!;
    public string Recipient {  get; set; } = null!;
    public string Text { get; set; } = null!;
}

public static class GreetingExtensions
{
    public static string ToStringWithConsumer(this Greeting greeting, string consumer)
    {
        return $@"Consumer: {consumer}
                    From: {greeting.Sender}
                    To: {greeting.Recipient}
                    Text: {greeting.Text}";
    }
    public static GreetingDTO ToDTO(this Greeting greeting, string consumer)
    {
        return new GreetingDTO
        {
            Sender = greeting.Sender,
            Recipient = greeting.Recipient,
            Text = greeting.Text,
            Consumer = consumer
        };
    }
}