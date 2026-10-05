using Deskflow.Api.Exceptions;
using Deskflow.Api.Models.Enums;

namespace Deskflow.Api.Models.Entities
{

    public class Chamado
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public Prioridade Prioridade { get; set; }
        public StatusChamado Status { get; set; }
        public string SolicitanteNome { get; set; } = string.Empty;
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public string? Solucao { get; set; }
        public string CategoriaId { get; set; } = string.Empty;
        public Categoria? Categoria { get; private set; }
        public List<Interacao> Interacoes { get; private set; } = new();

        public void Abrir()
        {
            if (string.IsNullOrWhiteSpace(Titulo) || string.IsNullOrWhiteSpace(Descricao)
                || string.IsNullOrWhiteSpace(SolicitanteNome) || string.IsNullOrWhiteSpace(CategoriaId))
                throw new RegraNegocioException("Titulo, Descricao, SolicitanteNome e CategoriaId são obrigatórios.");

            if (!Enum.IsDefined(Prioridade))
                throw new RegraNegocioException("Prioridade inválida. Use Baixa, Media ou Alta.");

            Id = 0;
            Status = StatusChamado.Aberto;
            DataAbertura = DateTime.Now;
            DataFechamento = null;
            Solucao = null;
            Categoria = null;
            Interacoes = new();
        }

        public void Iniciar()
        {
            if (Status != StatusChamado.Aberto)
                throw new RegraNegocioException("Somente chamados com status Aberto podem ser iniciados.");

            Status = StatusChamado.EmAndamento;
        }

        public Interacao AdicionarInteracao(string? autor, string? mensagem)
        {
            if (Status == StatusChamado.Fechado)
                throw new RegraNegocioException("Não é possível adicionar interações a um chamado fechado.");

            if (string.IsNullOrWhiteSpace(autor) || string.IsNullOrWhiteSpace(mensagem))
                throw new RegraNegocioException("Autor e Mensagem são obrigatórios.");

            var interacao = new Interacao
            {
                Autor = autor,
                Mensagem = mensagem,
                DataRegistro = DateTime.Now
            };
            Interacoes.Add(interacao);
            return interacao;
        }

        public void Encerrar(string? solucao)
        {
            if (Status == StatusChamado.Fechado)
                throw new RegraNegocioException("O chamado já está fechado.");

            if (string.IsNullOrWhiteSpace(solucao))
                throw new RegraNegocioException("A solução é obrigatória para encerrar o chamado.");

            Solucao = solucao;
            DataFechamento = DateTime.Now;
            Status = StatusChamado.Fechado;
        }
    }
}
