using DevHabit.Api;
using DevHabit.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddControllers()
    .AddErrorHandling()
    .AddDatabase()
    .AddObservability()
    .AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    await app.ApplyMigrationAsync();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.MapControllers();

await app.RunAsync();