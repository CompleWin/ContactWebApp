using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

public class HelloController : BaseController
{
    [HttpGet("hello/{name}")]
    public string GetGreetingByName(string name) => $"Привет, {name}!";
    
}