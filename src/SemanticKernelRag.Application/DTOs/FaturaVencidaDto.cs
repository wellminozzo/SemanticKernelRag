namespace SemanticKernelRag.Application.DTOs;

public class FaturaVencidaDto
{
    public int Id { get; set; }

    public string Cliente { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public DateTime DataVencimento { get; set; }

    public int DiasEmAtraso { get; set; }
} 
