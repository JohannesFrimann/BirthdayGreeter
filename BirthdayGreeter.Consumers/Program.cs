using BirthdayGreeter.Consumers.Consumers;
using BirthdayGreeter.Consumers.Hubs;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost5500",
        builder => builder.WithOrigins("http://localhost:5500") // Allow requests from this origin
                          .AllowAnyMethod() // Allow any HTTP method
                          .AllowAnyHeader() // Allow any HTTP header
                          .AllowCredentials()); // Allow credentials if needed

    options.AddPolicy("Allow5500",
        builder => builder.WithOrigins("http://127.0.0.1:5500") // Allow requests from this origin
                          .AllowAnyMethod() // Allow any HTTP method
                          .AllowAnyHeader() // Allow any HTTP header
                          .AllowCredentials()); // Allow credentials if needed
});
builder.Services.AddSignalR();
builder.Services.AddMassTransit(x => 
{
    x.AddConsumer<GreetingsSendConsumer>();
    x.AddConsumer<GreetingsPublishConsumer>();
    x.AddConsumer<GreetingsPublishConsumer1>();
    x.AddConsumer<GreetingsPublishConsumer2>();
    x.AddConsumer<GreetingsPublishConsumer3>();
    x.AddConsumer<BadLanguagePublishConsumer>();
    x.AddConsumer<CheckGreetingStatusConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {

        /*cfg.Host("goose.rmq2.cloudamqp.com", "zifuhpze", h =>
        {
            h.Username("zifuhpze");
            h.Password("JB1-kD3eB-xUE9CUpqqtM3mdjq8bO_-I");
        });*/
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("very-special-greeting", e => {
            e.ConfigureConsumer<GreetingsSendConsumer>(context);
        });
        cfg.ConfigureEndpoints(context);
    });
});



var app = builder.Build();
app.UseCors("Allow5500");
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<BirthdayHub>("/birthdayHub");
});
app.Run();