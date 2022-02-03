namespace ContosoRESTAPI.Data;

public static class Extensions
{
    /// <summary>
    /// Extension to seed the database if it hasn't already been done
    /// </summary>
    public static void CreateDbIfNotExists(this IHost host)
    {
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<PizzaContext>();
                // // https://stackoverflow.com/a/68796048
                // context.Database.EnsureDeleted();
                if (context.Database.EnsureCreated())
                {
                    DbInitializer.Initialize(context);
                }
            }
        }
    }
}
