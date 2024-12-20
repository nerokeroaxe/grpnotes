using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Web.Extensions;

public static class DbConnectionServiceExtension
{
    public static void AddDbConnectionService(this IServiceCollection services)
    {
        var host        = Environment.GetEnvironmentVariable("DB_HOST");
        var database    = Environment.GetEnvironmentVariable("DB_NAME");
        var username    = Environment.GetEnvironmentVariable("DB_USER");
        var password    = Environment.GetEnvironmentVariable("DB_PASSWORD");
        var connectionString = $"Host={host};Database={database};Username={username};Password={password}";
        
        services.AddDbContext<AppDatabase>(options => options.UseNpgsql(connectionString));
        services.AddSingleton<IDbContextFactory<AppDatabase>, DbContextFactory>();
    }

    public static void MigrateDb(this IApplicationBuilder app)
    {
        try 
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDatabase>>();
            using var db = context.CreateDbContext();
            db.Database.Migrate();
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }
    }
}