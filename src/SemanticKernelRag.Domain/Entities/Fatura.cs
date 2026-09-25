namespace SemanticKernelRag.Domain.Entities;

public class Fatura
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public decimal Valor { get; set; }

    public DateTime DataEmissao { get; set; }

    public DateTime DataVencimento { get; set; }

    public bool Pago { get; set; }

    public Cliente Cliente { get; set; } = null!;
}