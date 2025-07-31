using JobProcessor.Consumers;
using JobProcessor.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IJobProcessingService, JobProcessingService>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<JobCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitmqConfig = builder.Configuration.GetSection("RabbitMQ");
        cfg.Host(rabbitmqConfig["Host"], "/", h =>
        {
            h.Username(rabbitmqConfig["Username"]);
            h.Password(rabbitmqConfig["Password"]);
        });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();