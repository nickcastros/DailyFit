using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyFit.Controllers;

[ApiController]
[Route("[controller]")]
public class AcessoController : ControllerBase
{
    public AcessoController()
    {
    }


    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        return Ok("Acesso Permitido!!!"); 
    }
}