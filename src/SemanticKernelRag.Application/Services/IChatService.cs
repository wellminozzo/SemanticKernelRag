using SemanticKernelRag.Application.DTOs;

namespace SemanticKernelRag.Application.Services;

public interface IChatService
{
    Task<ChatResponse> SendMessageAsync(
        ChatRequest request,
        CancellationToken cancellationToken = default
    );
}