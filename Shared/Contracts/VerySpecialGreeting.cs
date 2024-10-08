using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts;

public class VerySpecialGreeting
{
    public string Sender { get; set; } = null!;
    public string Recipient { get; set; } = null!;
    public string Text { get; set; } = null!;
}


public static class VerySpecialGreetingExtensions
{
    public static string ToStringWithConsumer(this VerySpecialGreeting greeting, string consumer)
    {
        return @$"---------------------------{consumer} START-----------------------------
                    BirthdayGreeting from: {greeting.Sender}
                    To: {greeting.Recipient}
                    Text: {greeting.Text}
                    ---------------------------{consumer} END-----------------------------";
    }

    public static GreetingDTO ToDTO(this VerySpecialGreeting greeting, string consumer)
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