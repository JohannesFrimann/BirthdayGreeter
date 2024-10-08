using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts;

public class GreetingDTO
{
    public string Sender { get; set; } = null!;
    public string Recipient { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string Consumer { get; set; }
}
