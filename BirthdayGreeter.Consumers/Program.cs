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

        /*cfg.Host("goose.rmq2.cloudamqp.com", "dfxmgxxk", h =>
        {
            h.Username("dfxmgxxk");
            h.Password("Kd0ddfCp5jiyM8glBKKGzLaPKT_MAZdU");
        });*/
        cfg.Host("goose.rmq2.cloudamqp.com", "whbpcxrb", h =>
        {
            h.Username("whbpcxrb");
            h.Password("IH5o7UC2lmhVcpfKwa-iJu51ZkLyF-5y");
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