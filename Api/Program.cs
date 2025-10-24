using Api.Extensions;
using Api.Storage;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Services.AddServiceCollection(builder.Configuration);
        
        WebApplication app = builder.Build();
        
        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.Services.AddFakerService(builder.Configuration);
        
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