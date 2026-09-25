using Microsoft.AspNetCore.Mvc;
using SemanticKernelRag.Application.DTOs;
using SemanticKernelRag.Application.Services;

namespace SemanticKernelRag.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FaturasController : ControllerBase
{
    private readonly IFaturaService _faturaService;

    public FaturasController(IFaturaService faturaService)
    {
        _faturaService = faturaService;
    }

    [HttpGet("Vencidas")]
    public async Task<ActionResult<List<FaturaVencidaDto>>> ObterVencidasAsync(CancellationToken cancellationToken)
    {
        var faturas = await _faturaService.ObterVencidasAsync(cancellationToken);

        return Ok(faturas);
    }
}