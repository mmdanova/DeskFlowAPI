using Deskflow.Api.Models.Entities;
using Deskflow.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Deskflow.Api.Controllers
{
    [ApiController]
    [Route("api/chamados")]
    public class ChamadosController : ControllerBase
    {
        private IChamadoService _chamadoService;

        public ChamadosController(IChamadoService chamadoService)
        {
            _chamadoService = chamadoService;
        }

        [HttpPost]
        public async Task<IActionResult> AbrirAsync([FromBody] Chamado chamado)
        {
            var criado = await _chamadoService.AbrirAsync(chamado);
            return Created($"/api/chamados/{criado.Id}", criado);
        }

        [HttpPost("{id}/iniciar")]
        public async Task<IActionResult> IniciarAsync([FromRoute] int id)
        {
            var chamado = await _chamadoService.IniciarAsync(id);
            return Ok(chamado);
        }

        [HttpPost("{id}/encerrar")]
        public async Task<IActionResult> EncerrarAsync([FromRoute] int id, [FromBody] Chamado chamado)
        {
            var encerrado = await _chamadoService.EncerrarAsync(id, chamado.Solucao);
            return Ok(encerrado);
        }
    }
}
