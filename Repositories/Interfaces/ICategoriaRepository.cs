using Deskflow.Api.Models.Entities;

namespace Deskflow.Api.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> ObterTodosAsync();
        Task<Categoria> ObterPorIdAsync(string id);
        Task InserirAsync(Categoria categoria);
        Task Deletar(Categoria categoria);
        Task Atualizar(Categoria categoria);
        Task<bool> PossuiChamadosAsync(string id);
    }

}