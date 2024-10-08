using BirthdayGreeter.Producers.Api.Services;
using MassTransit;
using BirthdayGreeter.Producers.Api.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMassTransit(x =>
{
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


        cfg.ConfigureEndpoints(context);
    });
});

// Using the Mediator pattern
builder.Services.AddMediator(cfg =>
{
    cfg.AddConsumer<MediatorConsumer>();
});

// Add services to DI
builder.Services.AddScoped<BirthdayGreetingsService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api");
    c.RoutePrefix = string.Empty;
});

app.Run();
