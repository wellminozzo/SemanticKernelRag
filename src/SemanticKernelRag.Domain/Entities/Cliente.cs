namespace SemanticKernelRag.Domain.Entities;

public class Cliente
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public bool Ativo { get; set; } = true;

    public DateTime DataCadastro { get; set; }

    public ICollection<Chamado> Chamados { get; set; } = new List<Chamado>();

    public ICollection<Fatura> Faturas { get; set; } = new List<Fatura>();
    
}