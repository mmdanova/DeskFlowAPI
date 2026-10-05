using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Deskflow.Api.Services.Interfaces;
using Deskflow.Api.Models.Entities;


namespace Deskflow.Api.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriasController: ControllerBase
    {

        private ICategoriaService _categoriasService;

        public CategoriasController(ICategoriaService categoriasService)
        {
            _categoriasService = categoriasService;
        }


        [HttpPost]
        public async Task<IActionResult> CriarAsync([FromBody] Categoria categoria)
        {
            await _categoriasService.InserirAsync(categoria);
            return Created($"/api/categorias/{categoria.Id}", categoria);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosAsync()
        {

            var auth = Request.Headers["Authorization"].ToString();

                List<Categoria> categorias =  await _categoriasService.ObterTodosAsync();
                return Ok(categorias); 

        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObterPorIdAsync([FromRoute]string id)
        {
            //Categoria categoria = await _contexto.Categorias.FindAsync(id);
            Categoria categoria = await _categoriasService.ObterPorIdAsync(id);
            return Ok(categoria);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAscync([FromRoute] string id)
        {   
            await _categoriasService.Deletar(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]string id, [FromBody]Categoria categoriaAtualizada)
        {
            await _categoriasService.Update(categoriaAtualizada, id);
            return NoContent();
        }
    }
}