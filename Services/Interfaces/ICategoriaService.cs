
using Deskflow.Api.Models.Entities;

namespace Deskflow.Api.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<Categoria>> ObterTodosAsync();
        Task<Categoria> ObterPorIdAsync(string id);
        Task InserirAsync(Categoria categoria);

        Task Deletar (string id);
        Task Update(Categoria categoriaAtualizada, string id);
    }
}