namespace SemanticKernelRag.Infrastructure.AI;

public class OpenAIOptions
{
    public const string SectionName = "OpenAI";

    public string ApiKey {get; set;} = string.Empty;
    public string ModelId {get; set;} = string.Empty;
}