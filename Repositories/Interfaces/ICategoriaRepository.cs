using Deskflow.Api.Models.Entities;

namespace Deskflow.Api.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> ObterTodosAsync();
        Task<Categoria> ObterPorIdAsync(int id);
        Task InserirAsync(Categoria categoria);
        Task Deletar(Categoria categoria);
        Task Atualizar(Categoria categoria);
        Task<bool> PossuiChamadosAsync(int id);
    }

}