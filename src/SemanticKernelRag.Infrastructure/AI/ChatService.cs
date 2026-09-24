using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using SemanticKernelRag.Application.Services;

namespace SemanticKernelRag.Infrastructure.AI;

public class ChatService : IChatService
{
    private readonly IChatCompletionService _chatCompletionService;

    private readonly ChatHistory _history = new();


    public ChatService(Kernel kernel)
    {
        _chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

        _history.AddSystemMessage(
            """
            Você é um assistente empresarial,

            Responda de forma clara, objetiva e em português.

            Quando não souber uma informação, informe que não possui
            dados o suficiente para responder.

            Não invente informações.
            """
        );
    }

    public async Task<string> SendMessageAsync(
        string message,
        CancellationToken cancellationToken = default)
    {

        _history.AddUserMessage(message);
        var response = await _chatCompletionService.GetChatMessageContentAsync(
            _history,
            cancellationToken: cancellationToken
        );

        var content = response.Content ?? string.Empty;

        _history.AddAssistantMessage(content);

        return content;
    }
}