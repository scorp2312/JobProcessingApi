using JobCreator.Consumers;
using JobCreator.Data;
using JobCreator.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<JobService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<QuestionService>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<JobCompletedConsumer>();
    x.AddConsumer<JobInProgressConsumer>();

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
    builder.Configuration.AddUserSecrets<Program>(optional: true);
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Автоматическая миграция базы данных при запуске
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
    await DataSeeder.SeedDataAsync(context);
}

app.Run();