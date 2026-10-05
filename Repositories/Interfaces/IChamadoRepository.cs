using Deskflow.Api.Models.Entities;
using Deskflow.Api.Models.Enums;

namespace Deskflow.Api.Repositories.Interfaces
{
    public interface IChamadoRepository
    {
        Task InserirAsync(Chamado chamado);
        Task<Chamado?> ObterPorIdAsync(int id);
        Task<Chamado?> ObterDetalhadoPorIdAsync(int id);
        Task<List<Chamado>> ListarAsync(StatusChamado? status, Prioridade? prioridade, int? categoriaId);
        Task Atualizar(Chamado chamado);
    }
}
