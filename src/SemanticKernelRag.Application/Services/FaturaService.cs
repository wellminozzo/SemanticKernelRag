using SemanticKernelRag.Application.DTOs;
using SemanticKernelRag.Application.Repositories;

namespace SemanticKernelRag.Application.Services;

public class FaturaService : IFaturaService
{
    private readonly IFaturaRepository _repository;

    public FaturaService(IFaturaRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<FaturaVencidaDto>> ObterVencidasAsync(CancellationToken cancellationToken = default)
    {
        var faturas = await _repository.ObterVencidasAsync(cancellationToken);

        var hoje = DateTime.UtcNow.Date;

        return faturas 
            .Select(x => new FaturaVencidaDto
            {
                Id = x.Id,
                Cliente = x.Cliente.Nome,
                Valor = x.Valor,
                DataVencimento = x.DataVencimento,
                DiasEmAtraso = (hoje - x.DataVencimento.Date).Days
            }) 
            .ToList();
    } 
}