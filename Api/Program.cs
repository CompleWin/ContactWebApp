using Api.Storage;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        
        string hostUrl = args.Length > 0 ? args[0] : "http://localhost:3000";
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddSingleton<DataContext>();
        builder.Services.AddSingleton<ContactStorage>();

        builder.Services.AddCors(opt => 
            opt.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(hostUrl);
            })
            );
        
        WebApplication app = builder.Build();
        
        app.UseSwagger();
        app.UseSwaggerUI();

        
        // Это endpoints
        // app.MapGet("/test", () => "Hello World!");
        // app.MapGet("/hello/{name}", (string name) => $"Hello, {name}!");

        // Чтобы все HTTP запросы, которые будут приходить на наш сервис
        // перепровлялись по соотвествующим маршрутам контроллеров и после этого
        // обрабатывались контроллерами в нашем приложении 
        app.MapControllers();
        app.UseCors("CorsPolicy");
        
        app.Run();
    }
}