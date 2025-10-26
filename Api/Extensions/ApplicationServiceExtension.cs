using Api.DataContext;
using Api.Seed;
using Api.Storage;
using Microsoft.EntityFrameworkCore;

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
        
        service.AddDbContext<SqliteDbContext>(opt => opt.UseSqlite(connectionString));
        // service.AddSingleton<IStorage>(new SqliteStorage(connectionString));
        service.AddScoped<IPaginationStorage, SqlitePaginationEfStorage>();
        service.AddScoped<IInitializer, SqliteEfFakerInitializer>();
        
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