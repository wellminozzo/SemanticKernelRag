using System.Collections.Concurrent;
using Microsoft.SemanticKernel.ChatCompletion;

namespace SemanticKernelRag.Infrastructure.AI;

public class ChatHistoryStore
{
    private readonly ConcurrentDictionary<string, ChatHistory> _histories = new ();

    public ChatHistory GetOrCreate(string conversationId)
    {
        return _histories.GetOrAdd(
            conversationId,
            _ => CreateHistory());
    }

    private static ChatHistory CreateHistory()
    {
        var history = new ChatHistory();

        history.AddSystemMessage(
            """

            Você é um assistente empresarial integrado a um sistema corporativo.

            Regras:
            - Responda sempre em português do Brasil.
            - Seja claro, direto e objetivo.
            - Responda somente ao que foi perguntado.
            - Não ofereça serviços, oportunidades ou sugestões que não foram solicitadas.
            - Não invente informações.
            - Quando não possuir informações suficientes, diga claramente que não possui dados suficientes.
            - Utilize apenas informações fornecidas na conversa ou recuperadas pelas ferramentas disponíveis.
            """
        );

        return history;
    }
}
