namespace ContosoRESTAPI.Data;

public static class Extensions
{
    /// <summary>
    /// Extension to seed the database if it hasn't already been done
    /// </summary>
    public static void CreateDbIfNotExists(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<PizzaContext>();
            // EnsureCreated creates a new database if one doesn't exist.
            // The new database is NOT configured for migrations.
            bool databaseExists = context.Database.EnsureCreated();
            if (databaseExists)
            {
                DbInitializer.Initialize(context);
            }
        }
    }
}
