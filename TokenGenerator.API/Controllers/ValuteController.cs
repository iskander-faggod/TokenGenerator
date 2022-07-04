using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenGenerator.Domain.Models;
using TokenGenerator.Services.Interfaces;

namespace TokenGenerator.Controllers;

[Authorize]
[Route("api/quotation")]
[ApiController]
public class ValuteController : ControllerBase
{
    private IValuteService _valuteService;

    public ValuteController(IValuteService valuteService)
    {
        _valuteService = valuteService;
    }

    [HttpGet]
    public ValuteCurs GetValuteCurseByDate([FromQuery] DateTime dateTime)
    {
       return _valuteService.GetValuteData(dateTime);
    }

}