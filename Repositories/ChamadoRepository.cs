using Deskflow.Api.Data.Entities;
using Deskflow.Api.Models.Entities;
using Deskflow.Api.Repositories.Interfaces;

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

        public async Task Atualizar(Chamado chamado)
        {
            _context.Chamados.Update(chamado);
            await _context.SaveChangesAsync();
        }
    }
}
