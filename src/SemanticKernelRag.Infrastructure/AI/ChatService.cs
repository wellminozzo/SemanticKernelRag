using System.Net.Cache;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Ollama;
using SemanticKernelRag.Application.DTOs;
using SemanticKernelRag.Application.Services;
using SemanticKernelRag.Infrastructure.AI.Plugins;

namespace SemanticKernelRag.Infrastructure.AI;

public class ChatService : IChatService
{
    private readonly IChatCompletionService _chatCompletionService;
    private readonly ChatHistoryStore _historyStore;
    private readonly Kernel _kernel;
    private readonly FinanceiroPlugin _financeiroPlugin;

    public ChatService(Kernel kernel, ChatHistoryStore historyStore, FinanceiroPlugin financeiroPlugin)
    {
        _kernel = kernel;

       _chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

       _historyStore = historyStore;

       _financeiroPlugin = financeiroPlugin;
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

        var kernel = _kernel.Clone();

        kernel.Plugins.AddFromObject(_financeiroPlugin, "Financeiro");   

        var executionSettings = new OllamaPromptExecutionSettings
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(
                options: new FunctionChoiceBehaviorOptions
                {
                    AllowConcurrentInvocation = false,
                    AllowParallelCalls = false
                }
            )
        }; 


        var response =
            await _chatCompletionService.GetChatMessageContentAsync(
                history,
                executionSettings,
                kernel,
                cancellationToken);

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