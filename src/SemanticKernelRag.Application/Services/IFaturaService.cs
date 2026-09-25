using SemanticKernelRag.Application.DTOs;

namespace SemanticKernelRag.Application.Services;

public interface IFaturaService
{
    Task<List<FaturaVencidaDto>> ObterVencidasAsync(CancellationToken cancellationToken = default);
}