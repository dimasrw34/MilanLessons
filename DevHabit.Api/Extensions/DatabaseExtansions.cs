using DevHabit.Api.Database;
using Microsoft.EntityFrameworkCore;

namespace DevHabit.Api.Extensions;

public static class DatabaseExtensions
{
    public static async Task ApplyMigrationAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        await using ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            await dbContext.Database.MigrateAsync();
            app.Logger.LogInformation("Database migration applied successfully.");
        }
        catch (Exception e)
        {
            app.Logger.LogError("An error occurred while applying database migrations");
            throw;
        }
    }
}