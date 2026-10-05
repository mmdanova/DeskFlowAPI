using Deskflow.Api.Models.Entities;
using Deskflow.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Deskflow.Api.Data.Entities
{
    // Dados de exemplo aplicados pela migration (dotnet ef database update)
    public static class SeedData
    {
        public static void Aplicar(ModelBuilder mb)
        {
            mb.Entity<Categoria>().HasData(
                new { Id = 1, Nome = "Hardware" },
                new { Id = 2, Nome = "Rede e Internet" },
                new { Id = 3, Nome = "Software" },
                new { Id = 4, Nome = "Acessos e Senhas" },
                // Categorias sem chamados (podem ser excluídas)
                new { Id = 5, Nome = "Impressoras" },
                new { Id = 6, Nome = "Telefonia" }
            );

            mb.Entity<Chamado>().HasData(
                // EmAndamento com 2 interações
                new
                {
                    Id = 1,
                    Titulo = "Notebook não liga",
                    Descricao = "O notebook não liga mesmo conectado na tomada. O LED de carga não acende.",
                    Prioridade = Prioridade.Alta,
                    Status = StatusChamado.EmAndamento,
                    SolicitanteNome = "Ana Souza",
                    DataAbertura = new DateTime(2026, 9, 28, 9, 15, 0),
                    CategoriaId = 1
                },
                // Aberto sem interações
                new
                {
                    Id = 2,
                    Titulo = "Wi-Fi caindo na sala de reuniões",
                    Descricao = "A conexão Wi-Fi cai várias vezes por hora na sala de reuniões do 2º andar.",
                    Prioridade = Prioridade.Media,
                    Status = StatusChamado.Aberto,
                    SolicitanteNome = "Bruno Lima",
                    DataAbertura = new DateTime(2026, 10, 1, 14, 30, 0),
                    CategoriaId = 2
                },
                // Aberto com 1 interação
                new
                {
                    Id = 3,
                    Titulo = "Instalação do pacote Office",
                    Descricao = "Preciso do Office instalado no computador novo do setor financeiro.",
                    Prioridade = Prioridade.Baixa,
                    Status = StatusChamado.Aberto,
                    SolicitanteNome = "Carla Mendes",
                    DataAbertura = new DateTime(2026, 10, 2, 10, 0, 0),
                    CategoriaId = 3
                },
                // Fechado com interação
                new
                {
                    Id = 4,
                    Titulo = "Senha do e-mail bloqueada",
                    Descricao = "Minha conta de e-mail foi bloqueada após várias tentativas de login.",
                    Prioridade = Prioridade.Alta,
                    Status = StatusChamado.Fechado,
                    SolicitanteNome = "Diego Rocha",
                    DataAbertura = new DateTime(2026, 9, 25, 8, 40, 0),
                    DataFechamento = new DateTime(2026, 9, 25, 11, 5, 0),
                    Solucao = "Conta desbloqueada e senha redefinida. Usuário orientado a ativar o MFA.",
                    CategoriaId = 4
                },
                // EmAndamento sem interações
                new
                {
                    Id = 5,
                    Titulo = "Monitor com listras na tela",
                    Descricao = "O monitor secundário apresenta listras verticais coloridas.",
                    Prioridade = Prioridade.Media,
                    Status = StatusChamado.EmAndamento,
                    SolicitanteNome = "Eduarda Alves",
                    DataAbertura = new DateTime(2026, 10, 3, 16, 20, 0),
                    CategoriaId = 1
                }
            );

            mb.Entity<Interacao>().HasData(
                new
                {
                    Id = 1,
                    ChamadoId = 1,
                    Autor = "Suporte - Marcos",
                    Mensagem = "Testei com outro carregador e o problema persiste. Vou abrir o equipamento.",
                    DataRegistro = new DateTime(2026, 9, 28, 10, 0, 0)
                },
                new
                {
                    Id = 2,
                    ChamadoId = 1,
                    Autor = "Suporte - Marcos",
                    Mensagem = "Fonte interna com defeito. Peça solicitada, previsão de chegada em 2 dias.",
                    DataRegistro = new DateTime(2026, 9, 28, 15, 30, 0)
                },
                new
                {
                    Id = 3,
                    ChamadoId = 3,
                    Autor = "Suporte - Juliana",
                    Mensagem = "Licença disponível. Instalação agendada para amanhã às 9h.",
                    DataRegistro = new DateTime(2026, 10, 2, 11, 20, 0)
                },
                new
                {
                    Id = 4,
                    ChamadoId = 4,
                    Autor = "Suporte - Juliana",
                    Mensagem = "Identidade do usuário confirmada por telefone. Iniciando desbloqueio.",
                    DataRegistro = new DateTime(2026, 9, 25, 10, 50, 0)
                }
            );
        }
    }
}
