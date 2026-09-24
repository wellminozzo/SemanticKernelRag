namespace SemanticKernelRag.Infrastructure.AI;

public class OllamaOptions
{
    public const string SectionName = "Ollama";

    public string ModelId { get; set; } = "phi3:mini";

    public string Endpoint { get; set; } =
        "http://localhost:11434";
}