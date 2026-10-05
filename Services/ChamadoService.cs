using Deskflow.Api.Exceptions;
using Deskflow.Api.Models.Entities;
using Deskflow.Api.Models.Enums;
using Deskflow.Api.Repositories.Interfaces;
using Deskflow.Api.Services.Interfaces;

namespace Deskflow.Api.Services
{
    public class ChamadoService : IChamadoService
    {
        private IChamadoRepository _chamadoRepository;
        private ICategoriaRepository _categoriaRepository;

        public ChamadoService(IChamadoRepository chamadoRepository, ICategoriaRepository categoriaRepository)
        {
            _chamadoRepository = chamadoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<Chamado> AbrirAsync(Chamado chamado)
        {
            chamado.Abrir();

            var categoria = await _categoriaRepository.ObterPorIdAsync(chamado.CategoriaId);
            if (categoria == null)
                throw new RegraNegocioException($"Categoria Id : {chamado.CategoriaId} não encontrada");

            await _chamadoRepository.InserirAsync(chamado);
            return chamado;
        }

        public async Task<List<Chamado>> ListarAsync(StatusChamado? status, Prioridade? prioridade, string? categoriaId)
        {
            return await _chamadoRepository.ListarAsync(status, prioridade, categoriaId);
        }

        public async Task<Chamado> ObterDetalhesAsync(int id)
        {
            return await _chamadoRepository.ObterDetalhadoPorIdAsync(id)
                ?? throw new NaoEncontradoException($"Chamado Id : {id} não encontrado");
        }

        public async Task<Chamado> IniciarAsync(int id)
        {
            var chamado = await ObterOuFalharAsync(id);
            chamado.Iniciar();
            await _chamadoRepository.Atualizar(chamado);
            return chamado;
        }

        public async Task<Chamado> EncerrarAsync(int id, string? solucao)
        {
            var chamado = await ObterOuFalharAsync(id);
            chamado.Encerrar(solucao);
            await _chamadoRepository.Atualizar(chamado);
            return chamado;
        }

        public async Task<Interacao> AdicionarInteracaoAsync(int id, string? autor, string? mensagem)
        {
            var chamado = await ObterOuFalharAsync(id);
            var interacao = chamado.AdicionarInteracao(autor, mensagem);
            await _chamadoRepository.Atualizar(chamado);
            return interacao;
        }

        private async Task<Chamado> ObterOuFalharAsync(int id)
        {
            return await _chamadoRepository.ObterPorIdAsync(id)
                ?? throw new NaoEncontradoException($"Chamado Id : {id} não encontrado");
        }
    }
}
