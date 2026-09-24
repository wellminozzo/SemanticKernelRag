using System.Net.Cache;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using SemanticKernelRag.Application.DTOs;
using SemanticKernelRag.Application.Services;

namespace SemanticKernelRag.Infrastructure.AI;

public class ChatService : IChatService
{
    private readonly IChatCompletionService _chatCompletionService;

    private readonly ChatHistoryStore _historyStore;


    public ChatService(Kernel kernel, ChatHistoryStore historyStore)
    {
       _chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

       _historyStore = historyStore;
    }

    public async Task<ChatResponse> SendMessageAsync(
        ChatRequest request,
        CancellationToken cancellationToken = default)
    {
        var conversationId =
            string.IsNullOrWhiteSpace(request.ConversationId)
                ? Guid.NewGuid().ToString()
                : request.ConversationId;

        var history =
            _historyStore.GetOrCreate(conversationId);

        history.AddUserMessage(request.Message);

        var response =
            await _chatCompletionService.GetChatMessageContentAsync(
                history,
                cancellationToken: cancellationToken);

        var content =
            response.Content ?? string.Empty;

        history.AddAssistantMessage(content);

        return new ChatResponse
        {
            ConversationId = conversationId,
            Message = content
        };
    }
}