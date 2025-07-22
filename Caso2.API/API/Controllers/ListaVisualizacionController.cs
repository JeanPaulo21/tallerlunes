using System;
using System.Linq;
using System.Threading.Tasks;
using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Flujo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListaVisualizacionController : ControllerBase, IListaVisualizacionController
    {
        private readonly IListaVisualizacionFlujo _listaVisualizacionFlujo;
        private readonly ILogger<ListaVisualizacionController> _logger;

        public ListaVisualizacionController(IListaVisualizacionFlujo listaVisualizacionFlujo, ILogger<ListaVisualizacionController> logger)
        {
            _listaVisualizacionFlujo = listaVisualizacionFlujo;
            _logger = logger;
        }
        #region "Operaciones"
        [HttpPost]
        public async Task<IActionResult> AgregarItem([FromBody] ListaVisualizacionRequest item)
        {
            var resultado = await _listaVisualizacionFlujo.AgregarItem(item);
            return CreatedAtAction(nameof(ObtenerItemPorId), new { itemId = resultado }, null);
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> EditarItem([FromRoute] Guid itemId, [FromBody] ListaVisualizacionRequest item)
        {
            if (!await VerificarItemExiste(itemId))
                return NotFound("El ítem no existe");

            var resultado = await _listaVisualizacionFlujo.EditarItem(itemId, item);
            return Ok(resultado);
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> EliminarItem([FromRoute] Guid itemId)
        {
            if (!await VerificarItemExiste(itemId))
                return NotFound("El ítem no existe");

            await _listaVisualizacionFlujo.EliminarItem(itemId);
            return NoContent();
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ObtenerLista([FromRoute] Guid usuarioId)
        {
            var resultado = await _listaVisualizacionFlujo.ObtenerLista(usuarioId);
            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("{itemId}")]
        public async Task<IActionResult> ObtenerItemPorId([FromRoute] Guid itemId)
        {
            var resultado = await _listaVisualizacionFlujo.ObtenerItemPorId(itemId);
            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerVisualizaciones()
        {
            try
            {
                var favoritos = await _listaVisualizacionFlujo.ObtenerVisualizaciones();

                if (favoritos == null || !favoritos.Any())
                    return NoContent();

                return Ok(favoritos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los favoritos");
                return StatusCode(500, "Error interno en el servidor");
            }
        }
        #endregion

        #region Helpers
        private async Task<bool> VerificarItemExiste(Guid itemId)
        {
            var item = await _listaVisualizacionFlujo.ObtenerItemPorId(itemId);
            return item != null;
        }
        #endregion
    }
}
