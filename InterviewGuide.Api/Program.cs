using InterviewGuide.Application.Services;
using InterviewGuide.Infrastructure.Data;
using InterviewGuide.Infrastructure.DependencyInjection;
using InterviewGuide.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<QuestionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<MyExceptionHandler>();
    app.UseSwagger();
    app.UseSwaggerUI();
    builder.Configuration.AddUserSecrets<Program>(optional: true);
}

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
    await DataSeeder.SeedDataAsync(context);
}

app.Run();