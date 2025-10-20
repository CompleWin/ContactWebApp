using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

public class TestController : BaseController
{
    [HttpGet("hello")]
    public string GetHelloWorldText() => "Привет!";
}