using Api.Storage;

namespace Api.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddServiceCollection(
        this IServiceCollection service, 
        ConfigurationManager configuration)
    {
        service.AddEndpointsApiExplorer();
        service.AddSwaggerGen();
        service.AddControllers();
        
        string connectionString = configuration.GetConnectionString("SqliteStringConnection");
        service.AddSingleton<IStorage>(new SqliteStorage(connectionString));

        service.AddCors(opt =>
            opt.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(configuration["client"]);
            }));
        
        return service;
    }
}