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

        public async Task Deletar (string id)
        {
            Categoria  categoria = await _categoriaRepository.ObterPorIdAsync(id);

            if(categoria != null)
            {
                await _categoriaRepository.Deletar(categoria);
            }  
        }

        public async Task InserirAsync(Categoria categoria)
        {
            await _categoriaRepository.InserirAsync(categoria);
        }


        public async Task<Categoria> ObterPorIdAsync(string id)
        {
            return await _categoriaRepository.ObterPorIdAsync(id);
        }

        public async Task<List<Categoria>> ObterTodosAsync() 
                => await _categoriaRepository.ObterTodosAsync();

        public async Task Update(Categoria categoriaAtualizada, string id)
        {
            var categoriaDb  = await _categoriaRepository.ObterPorIdAsync(id);
            
            if(categoriaDb == null)
            {
                //throw new NotFoundException($"categoria Id : {id} não encontrada"); 
                throw new($"categoria Id : {id} não encontrada"); 
            }

            categoriaDb.Update(categoriaAtualizada);

            await _categoriaRepository.Atualizar(categoriaDb);
        }


    }
}