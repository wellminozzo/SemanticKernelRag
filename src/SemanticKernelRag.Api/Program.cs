using SemanticKernelRag.Infrastructure.AI;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSemanticKernelServices(
    builder.Configuration);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

