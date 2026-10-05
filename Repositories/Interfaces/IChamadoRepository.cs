using Deskflow.Api.Models.Entities;

namespace Deskflow.Api.Repositories.Interfaces
{
    public interface IChamadoRepository
    {
        Task InserirAsync(Chamado chamado);
        Task<Chamado?> ObterPorIdAsync(int id);
        Task<Chamado?> ObterDetalhadoPorIdAsync(int id);
        Task Atualizar(Chamado chamado);
    }
}
