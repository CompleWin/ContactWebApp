using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

[Route("[controller]")]
public class FallbackController : Microsoft.AspNetCore.Mvc.Controller
{
    [HttpGet("/")]
    public IActionResult Index()
    {
        return PhysicalFile(
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "index.html"
            ), "text/HTML"
        );
    }
}