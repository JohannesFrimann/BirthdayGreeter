using BirthdayGreeter.Producers.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;
using System.Net;
namespace BirthdayGreeter.Producers.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BirtdayGreetingController : ControllerBase
{
    private readonly BirthdayGreetingsService _birthdayGreetingsService;
    public BirtdayGreetingController(BirthdayGreetingsService birthdayGreetingsService) 
    {
        _birthdayGreetingsService = birthdayGreetingsService;
    }

    /// <summary>
    /// Send message to a specific endpoint
    /// </summary>
    /// <param name="greeting"></param>
    /// <returns></returns>
    [HttpPost("SendMessage")]
    public async Task<IActionResult> SendMessage(VerySpecialGreeting greeting)
    {
        await _birthdayGreetingsService.SendMessage(greeting);
        return Ok();
    }
    /// <summary>
    /// Publish message to everyone that wants to consume it
    /// </summary>
    /// <param name="greeting"></param>
    /// <returns></returns>
    [HttpPost("PublishMessage")]
    public async Task<IActionResult> PublishMessage(Greeting greeting)
    {
        await _birthdayGreetingsService.PublishMessage(greeting);
        return Ok();
    }

    /// <summary>
    /// publish bad message and see the dead letter queue working (_error queue)
    /// </summary>
    /// <param name="greeting"></param>
    /// <returns></returns>
    [HttpPost("PublishMessageWithBadLanguage")]
    public async Task<IActionResult> PublishMessageWithBadLanguage(Greeting greeting)
    {
        await _birthdayGreetingsService.PublishMessageWithBadLanguage(greeting);
        return Ok();
    }


    /// <summary>
    /// request/response messaging
    /// </summary>
    /// <param name="greetingId"></param>
    /// <returns></returns>
    [HttpGet("{greetingId}/Status")]
    public async Task<IActionResult> RequestResponseStatusMessage(string greetingId)
    {
        try { 
            return Ok(await _birthdayGreetingsService.RequestResponseStatusMessage(greetingId));
        }
        catch (TaskCanceledException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, "The Greetingstatuscheck server seems to be very busy!");
        }
    }

    /// <summary>
    /// Show how the Mediator works
    /// </summary>
    /// <returns></returns>
    [HttpPost("Mediator/FireAndForget")]
    public async Task<IActionResult> MediatorExample(VeryLargeAndComplexGreeting greeting)
    {
        return Ok(await _birthdayGreetingsService.MediatorFireAndForgetMessage(greeting));
    }
}
