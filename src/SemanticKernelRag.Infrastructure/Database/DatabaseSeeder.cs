using Microsoft.EntityFrameworkCore;
using SemanticKernelRag.Domain.Entities;

namespace SemanticKernelRag.Infrastructure.Database;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(
        AppDbContext context,
        CancellationToken cancellationToken = default)
    {
        if (await context.Clientes.AnyAsync(cancellationToken))
        {
            return;
        }

        var cliente1 = new Cliente
        {
            Nome = "Tech Solutions Ltda",
            Email = "contato@techsolutions.com.br",
            Ativo = true,
            DataCadastro = DateTime.UtcNow.AddMonths(-12)
        };

        var cliente2 = new Cliente
        {
            Nome = "Comercial Horizonte",
            Email = "financeiro@horizonte.com.br",
            Ativo = true,
            DataCadastro = DateTime.UtcNow.AddMonths(-8)
        };

        var cliente3 = new Cliente
        {
            Nome = "Indústria Aurora",
            Email = "administrativo@aurora.com.br",
            Ativo = true,
            DataCadastro = DateTime.UtcNow.AddMonths(-5)
        };

        context.Clientes.AddRange(
            cliente1,
            cliente2,
            cliente3);

        await context.SaveChangesAsync(cancellationToken);

        var chamados = new[]
        {
            new Chamado
            {
                ClienteId = cliente1.Id,
                Titulo = "Erro ao gerar boleto",
                Descricao =
                    "Ao realizar o faturamento mensal, o sistema apresenta erro durante a geração do boleto bancário.",
                Status = "Aberto",
                DataAbertura = DateTime.UtcNow.AddDays(-10)
            },

            new Chamado
            {
                ClienteId = cliente2.Id,
                Titulo = "Pagamento não identificado",
                Descricao =
                    "O cliente realizou o pagamento do boleto, porém a fatura continua aparecendo como pendente no sistema.",
                Status = "Aberto",
                DataAbertura = DateTime.UtcNow.AddDays(-7)
            },

            new Chamado
            {
                ClienteId = cliente3.Id,
                Titulo = "Problema no acesso",
                Descricao =
                    "Usuários não conseguem acessar o sistema utilizando suas credenciais.",
                Status = "Fechado",
                DataAbertura = DateTime.UtcNow.AddDays(-20),
                DataFechamento = DateTime.UtcNow.AddDays(-19)
            },

            new Chamado
            {
                ClienteId = cliente1.Id,
                Titulo = "Segunda via de cobrança",
                Descricao =
                    "Cliente solicita uma nova via da cobrança porque não recebeu o boleto por e-mail.",
                Status = "Aberto",
                DataAbertura = DateTime.UtcNow.AddDays(-3)
            }
        };

        context.Chamados.AddRange(chamados);

        var faturas = new[]
        {
            new Fatura
            {
                ClienteId = cliente1.Id,
                Valor = 1500.00m,
                DataEmissao = DateTime.UtcNow.AddDays(-40),
                DataVencimento = DateTime.UtcNow.AddDays(-10),
                Pago = false
            },

            new Fatura
            {
                ClienteId = cliente1.Id,
                Valor = 2300.00m,
                DataEmissao = DateTime.UtcNow.AddDays(-20),
                DataVencimento = DateTime.UtcNow.AddDays(10),
                Pago = false
            },

            new Fatura
            {
                ClienteId = cliente2.Id,
                Valor = 850.00m,
                DataEmissao = DateTime.UtcNow.AddDays(-30),
                DataVencimento = DateTime.UtcNow.AddDays(-5),
                Pago = false
            },

            new Fatura
            {
                ClienteId = cliente3.Id,
                Valor = 3200.00m,
                DataEmissao = DateTime.UtcNow.AddDays(-50),
                DataVencimento = DateTime.UtcNow.AddDays(-20),
                Pago = true
            }
        };

        context.Faturas.AddRange(faturas);

        await context.SaveChangesAsync(cancellationToken);
    }
}