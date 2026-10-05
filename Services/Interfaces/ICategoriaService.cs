
using Deskflow.Api.Models.Entities;

namespace Deskflow.Api.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<Categoria>> ObterTodosAsync();
        Task<Categoria> ObterPorIdAsync(int id);
        Task InserirAsync(Categoria categoria);

        Task Deletar (int id);
        Task Update(Categoria categoriaAtualizada, int id);
    }
}