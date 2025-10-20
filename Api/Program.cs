using Api.Storage;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddSingleton<DataContext>();
        builder.Services.AddSingleton<ContactStorage>();
        
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
        
        app.Run();
    }
}