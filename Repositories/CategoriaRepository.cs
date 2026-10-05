using Deskflow.Api.Data.Entities;
using Deskflow.Api.Models.Entities;
using Deskflow.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Deskflow.Api.Repositories
{

    public class CategoriaRepository : ICategoriaRepository
    {
        private AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Atualizar(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task InserirAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<Categoria> ObterPorIdAsync(string id)
        {
            return await _context.Categorias.FindAsync(id); 
        }

        public async Task<List<Categoria>> ObterTodosAsync()
        {
            return await _context.Categorias.ToListAsync();
        }
    }
}