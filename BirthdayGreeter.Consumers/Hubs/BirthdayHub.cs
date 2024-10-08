using Microsoft.AspNetCore.SignalR;

namespace BirthdayGreeter.Consumers.Hubs;

public class BirthdayHub : Hub
{
    public async Task SendBirthdayNotification(string message)
    {
        await Clients.All.SendAsync("ReceiveBirthdayNotification", message);
    }

}