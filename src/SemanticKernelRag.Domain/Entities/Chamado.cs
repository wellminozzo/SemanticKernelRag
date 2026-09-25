namespace SemanticKernelRag.Domain.Entities;

public class Chamado
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public string Titulo { get; set;} = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime DataAbertura { get; set; }

    public DateTime? DataFechamento { get; set; }

    public Cliente Cliente { get; set; } = null!;
}