using Deskflow.Api.Exceptions;
using Deskflow.Api.Services.Interfaces;
using Deskflow.Api.Models.Entities;
using Deskflow.Api.Repositories.Interfaces;

namespace Deskflow.Api.Services
{
    public class CategoriaService : ICategoriaService
    {
        private ICategoriaRepository _categoriaRepository;

        public CategoriaService (ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task Deletar (int id)
        {
            Categoria categoria = await _categoriaRepository.ObterPorIdAsync(id)
                ?? throw new NaoEncontradoException($"Categoria Id : {id} não encontrada");

            if (await _categoriaRepository.PossuiChamadosAsync(id))
            {
                throw new RegraNegocioException("Não é possível excluir a categoria pois ela possui chamados associados.");
            }

            await _categoriaRepository.Deletar(categoria);
        }

        public async Task InserirAsync(Categoria categoria)
        {
            categoria.Id = 0; // Id gerado pelo banco (identity)
            await _categoriaRepository.InserirAsync(categoria);
        }


        public async Task<Categoria> ObterPorIdAsync(int id)
        {
            return await _categoriaRepository.ObterPorIdAsync(id)
                ?? throw new NaoEncontradoException($"Categoria Id : {id} não encontrada");
        }

        public async Task<List<Categoria>> ObterTodosAsync() 
                => await _categoriaRepository.ObterTodosAsync();

        public async Task Update(Categoria categoriaAtualizada, int id)
        {
            var categoriaDb  = await _categoriaRepository.ObterPorIdAsync(id);
            
            if(categoriaDb == null)
            {
                throw new NaoEncontradoException($"Categoria Id : {id} não encontrada");
            }

            categoriaDb.Update(categoriaAtualizada);

            await _categoriaRepository.Atualizar(categoriaDb);
        }


    }
}