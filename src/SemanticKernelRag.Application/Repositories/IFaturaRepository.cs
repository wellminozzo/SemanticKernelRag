using SemanticKernelRag.Domain.Entities;

namespace SemanticKernelRag.Application.Repositories;

public interface IFaturaRepository
{
    Task<List<Fatura>> ObterVencidasAsync(CancellationToken cancellationToken = default);
}