using Deskflow.Api.Models.Entities;
using Deskflow.Api.Models.Enums;

namespace Deskflow.Api.Services.Interfaces
{
    public interface IChamadoService
    {
        Task<Chamado> AbrirAsync(Chamado chamado);
        Task<Chamado> ObterDetalhesAsync(int id);
        Task<List<Chamado>> ListarAsync(StatusChamado? status, Prioridade? prioridade, string? categoriaId);
        Task<Chamado> IniciarAsync(int id);
        Task<Chamado> EncerrarAsync(int id, string? solucao);
        Task<Interacao> AdicionarInteracaoAsync(int id, string? autor, string? mensagem);
    }
}
