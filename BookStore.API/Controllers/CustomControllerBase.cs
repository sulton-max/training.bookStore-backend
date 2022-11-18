using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

[ApiController()]
[Route("api/[controller]")]
public class CustomControllerBase : ControllerBase
{
    protected readonly IWebHostEnvironment HstEnvironment;
    protected readonly ILogger Logger;

    public CustomControllerBase
    (
        IWebHostEnvironment hostEnvironment,
        ILogger logger
    )
    {
        HstEnvironment = hostEnvironment;
        Logger = logger;
    }
}
