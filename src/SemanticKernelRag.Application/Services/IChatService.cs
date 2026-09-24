namespace SemanticKernelRag.Application.Services;

public interface IChatService
{
    Task<string> SendMessageAsync(
        string message,
        CancellationToken cancellationToken = default
    );
}