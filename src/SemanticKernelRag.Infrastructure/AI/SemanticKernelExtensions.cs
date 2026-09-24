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

        services.AddSingleton(kernel);

        services.AddScoped<IChatService, ChatService>();

        return services;
    }
}