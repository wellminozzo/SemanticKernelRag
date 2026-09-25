using Microsoft.EntityFrameworkCore;
using SemanticKernelRag.Application.Repositories;
using SemanticKernelRag.Domain.Entities;
using SemanticKernelRag.Infrastructure.Database;

namespace SemanticKernelRag.Infrastructure.Repositories;   

public class FaturaRepository : IFaturaRepository
{
    private readonly AppDbContext _context;

    public FaturaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Fatura>> ObterVencidasAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Faturas
            .AsNoTracking()
            .Include(x => x.Cliente)
            .Where(x => !x.Pago && x.DataVencimento < DateTime.UtcNow)
            .OrderBy(x => x.DataVencimento)
            .ToListAsync(cancellationToken);
    }
}