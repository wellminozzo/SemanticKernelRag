using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using SemanticKernelRag.Application.Services;

namespace SemanticKernelRag.Infrastructure.AI;

public static class SemanticKernelExtensions
{
    public static IServiceCollection AddSemanticKernelServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var modelId =
            configuration["Ollama:ModelId"]
            ?? "phi3:mini";

        var endpoint =
            configuration["Ollama:Endpoint"]
            ?? "http://localhost:11434";

        var kernelBuilder = Kernel.CreateBuilder();

#pragma warning disable SKEXP0070

        kernelBuilder.AddOllamaChatCompletion(
            modelId: modelId,
            endpoint: new Uri(endpoint));

#pragma warning restore SKEXP0070

        var kernel = kernelBuilder.Build();

        //Singleton - Uma única instância durante toda a vida da aplicação. Para caches, configurações e serviços thread-safe.
        services.AddSingleton(kernel);
        services.AddSingleton<ChatHistoryStore>();

        //Scoped - Uma instância por requisição HTTP. Padrão para repositórios e DbContext — garante consistência dentro de um request.
        services.AddScoped<IChatService, ChatService>();

        //Transient - Nova instância a cada injeção. Ideal para serviços leves, stateless e sem estado compartilhado entre chamadas.

        return services;
    }
}