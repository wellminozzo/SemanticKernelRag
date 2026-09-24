using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel.ChatCompletion;
using SemanticKernelRag.Application.DTOs;
using SemanticKernelRag.Application.Services;

namespace SemanticKernelRag.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost]
    public async Task<ActionResult<ChatResponse>> SendMessage([FromBody] ChatRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest("A mensagem é obrigatória.");
        }

        var response = await _chatService.SendMessageAsync(
            request,
            cancellationToken
        );

        return Ok(response);
    }
    
}