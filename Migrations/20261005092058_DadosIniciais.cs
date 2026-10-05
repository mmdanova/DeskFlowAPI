using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DeskFlow.API.Migrations
{
    /// <inheritdoc />
    public partial class DadosIniciais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tb_categorias",
                columns: new[] { "codCategoria", "nomeCategoria" },
                values: new object[,]
                {
                    { 1, "Hardware" },
                    { 2, "Rede e Internet" },
                    { 3, "Software" },
                    { 4, "Acessos e Senhas" },
                    { 5, "Impressoras" },
                    { 6, "Telefonia" }
                });

            migrationBuilder.InsertData(
                table: "Chamados",
                columns: new[] { "Id", "CategoriaId", "DataAbertura", "DataFechamento", "Descricao", "Prioridade", "SolicitanteNome", "Solucao", "Status", "Titulo" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 9, 28, 9, 15, 0, 0, DateTimeKind.Unspecified), null, "O notebook não liga mesmo conectado na tomada. O LED de carga não acende.", "Alta", "Ana Souza", null, "EmAndamento", "Notebook não liga" },
                    { 2, 2, new DateTime(2026, 10, 1, 14, 30, 0, 0, DateTimeKind.Unspecified), null, "A conexão Wi-Fi cai várias vezes por hora na sala de reuniões do 2º andar.", "Media", "Bruno Lima", null, "Aberto", "Wi-Fi caindo na sala de reuniões" },
                    { 3, 3, new DateTime(2026, 10, 2, 10, 0, 0, 0, DateTimeKind.Unspecified), null, "Preciso do Office instalado no computador novo do setor financeiro.", "Baixa", "Carla Mendes", null, "Aberto", "Instalação do pacote Office" },
                    { 4, 4, new DateTime(2026, 9, 25, 8, 40, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 9, 25, 11, 5, 0, 0, DateTimeKind.Unspecified), "Minha conta de e-mail foi bloqueada após várias tentativas de login.", "Alta", "Diego Rocha", "Conta desbloqueada e senha redefinida. Usuário orientado a ativar o MFA.", "Fechado", "Senha do e-mail bloqueada" },
                    { 5, 1, new DateTime(2026, 10, 3, 16, 20, 0, 0, DateTimeKind.Unspecified), null, "O monitor secundário apresenta listras verticais coloridas.", "Media", "Eduarda Alves", null, "EmAndamento", "Monitor com listras na tela" }
                });

            migrationBuilder.InsertData(
                table: "Interacoes",
                columns: new[] { "Id", "Autor", "ChamadoId", "DataRegistro", "Mensagem" },
                values: new object[,]
                {
                    { 1, "Suporte - Marcos", 1, new DateTime(2026, 9, 28, 10, 0, 0, 0, DateTimeKind.Unspecified), "Testei com outro carregador e o problema persiste. Vou abrir o equipamento." },
                    { 2, "Suporte - Marcos", 1, new DateTime(2026, 9, 28, 15, 30, 0, 0, DateTimeKind.Unspecified), "Fonte interna com defeito. Peça solicitada, previsão de chegada em 2 dias." },
                    { 3, "Suporte - Juliana", 3, new DateTime(2026, 10, 2, 11, 20, 0, 0, DateTimeKind.Unspecified), "Licença disponível. Instalação agendada para amanhã às 9h." },
                    { 4, "Suporte - Juliana", 4, new DateTime(2026, 9, 25, 10, 50, 0, 0, DateTimeKind.Unspecified), "Identidade do usuário confirmada por telefone. Iniciando desbloqueio." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Interacoes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Interacoes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Interacoes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Interacoes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tb_categorias",
                keyColumn: "codCategoria",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tb_categorias",
                keyColumn: "codCategoria",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tb_categorias",
                keyColumn: "codCategoria",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tb_categorias",
                keyColumn: "codCategoria",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tb_categorias",
                keyColumn: "codCategoria",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tb_categorias",
                keyColumn: "codCategoria",
                keyValue: 4);
        }
    }
}
