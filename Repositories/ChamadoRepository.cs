using Deskflow.Api.Data.Entities;
using Deskflow.Api.Models.Entities;
using Deskflow.Api.Models.Enums;
using Deskflow.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Deskflow.Api.Repositories
{
    public class ChamadoRepository : IChamadoRepository
    {
        private AppDbContext _context;

        public ChamadoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task InserirAsync(Chamado chamado)
        {
            await _context.Chamados.AddAsync(chamado);
            await _context.SaveChangesAsync();
        }

        public async Task<Chamado?> ObterPorIdAsync(int id)
        {
            return await _context.Chamados.FindAsync(id);
        }

        public async Task<Chamado?> ObterDetalhadoPorIdAsync(int id)
        {
            return await _context.Chamados
                .Include(c => c.Categoria)
                .Include(c => c.Interacoes.OrderBy(i => i.DataRegistro))
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Chamado>> ListarAsync(StatusChamado? status, Prioridade? prioridade, string? categoriaId)
        {
            var query = _context.Chamados.AsNoTracking().Include(c => c.Categoria).AsQueryable();

            if (status.HasValue)
                query = query.Where(c => c.Status == status.Value);
            if (prioridade.HasValue)
                query = query.Where(c => c.Prioridade == prioridade.Value);
            if (!string.IsNullOrWhiteSpace(categoriaId))
                query = query.Where(c => c.CategoriaId == categoriaId);

            return await query.OrderByDescending(c => c.DataAbertura).ToListAsync();
        }

        public async Task Atualizar(Chamado chamado)
        {
            _context.Chamados.Update(chamado);
            await _context.SaveChangesAsync();
        }
    }
}
