using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts;

public class CheckGreetingStatus
{
    public string GreetingId {  get; set; }
}

public class GreetingStatusResult
{
    public string GreetingId { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Status {  get; set; }
}
