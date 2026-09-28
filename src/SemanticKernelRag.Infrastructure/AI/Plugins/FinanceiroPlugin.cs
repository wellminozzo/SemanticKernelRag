using System.ComponentModel;
using Microsoft.SemanticKernel;
using SemanticKernelRag.Application.Services;


namespace SemanticKernelRag.Infrastructure.AI.Plugins;

public class FinanceiroPlugin
{
    private readonly IFaturaService _faturaService;

    public FinanceiroPlugin(IFaturaService faturaService)
    {
        _faturaService = faturaService;
    }

    [KernelFunction("obter_faturas_vencidas")]
    [Description("Obtém as faturas vencidas e ainda não pagas dos clientes.")]
    public async Task<string> ObterFaturasVencidasAsync(CancellationToken cancellationToken = default)
    {

        Console.WriteLine(">>> FinanceiroPlugin.ObterFaturasVencidasAsync EXECUTADO");
        
        var faturas = await _faturaService.ObterVencidasAsync(cancellationToken);
        
        if(faturas.Count == 0)
        {
            return "Não existem faturas vencidas.";
        }

        var resultado = faturas.Select(f =>
        $"Cliente: {f.Cliente}, " +
        $"Valor: R$ {f.Valor:F2}, " +
        $"Vencimento: {f.DataVencimento:dd/MM/yyyy}, " +
        $"Dias em atraso: {f.DiasEmAtraso}");

        return string.Join(Environment.NewLine, resultado);
    }
}
