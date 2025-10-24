using Api.Seed;
using Api.Storage;

namespace Api.Extensions;

public static class ServiceProviderExtension
{
    public static IServiceProvider AddFakerService(this IServiceProvider services,
        ConfigurationManager configuration)
    {
        // Экземпляр возможных, зарегистрированных сервисов
        using IServiceScope scope = services.CreateScope();
        
        // Вытаскиваем конкретный IStorage
        IStorage storage = scope.ServiceProvider.GetService<IStorage>();
        
        SqliteStorage dbStorage = storage as SqliteStorage;

        if (dbStorage != null)
        {
            string connectionString = configuration.GetConnectionString("SqliteStringConnection");
            new FakerInitializer(connectionString).Initialize();
        }
        
        return services;
    }
}