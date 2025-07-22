using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritoController : ControllerBase, IFavoritoController
    {
        private readonly IFavoritoFlujo _favoritoFlujo;
        private readonly ILogger<FavoritoController> _logger;

        public FavoritoController(IFavoritoFlujo favoritoFlujo, ILogger<FavoritoController> logger)
        {
            _favoritoFlujo = favoritoFlujo;
            _logger = logger;
        }

        #region Operaciones

        // POST api/favorito
        [HttpPost]
        public async Task<IActionResult> AgregarFavorito([FromBody] FavoritoRequest favorito)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var nuevoId = await _favoritoFlujo.AgregarFavorito(favorito);
                return CreatedAtAction(nameof(ObtenerFavoritoPorId), new { favoritoId = nuevoId }, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error agregando favorito");
                return StatusCode(500, "Error interno en el servidor");
            }
        }

        // PUT api/favorito/{favoritoId}
        [HttpPut("{favoritoId}")]
        public async Task<IActionResult> EditarFavorito([FromRoute] Guid favoritoId, [FromBody] FavoritoRequest favorito)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await VerificarFavoritoExiste(favoritoId))
                return NotFound($"El favorito con ID {favoritoId} no existe");

            try
            {
                var resultado = await _favoritoFlujo.EditarFavorito(favoritoId, favorito);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error editando favorito");
                return StatusCode(500, "Error interno en el servidor");
            }
        }

        // DELETE api/favorito/{favoritoId}
        [HttpDelete("{favoritoId}")]
        public async Task<IActionResult> EliminarFavorito([FromRoute] Guid favoritoId)
        {
            if (!await VerificarFavoritoExiste(favoritoId))
                return NotFound($"El favorito con ID {favoritoId} no existe");

            try
            {
                await _favoritoFlujo.EliminarFavorito(favoritoId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando favorito");
                return StatusCode(500, "Error interno en el servidor");
            }
        }

        // GET api/favorito/usuario/{usuarioId}
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ObtenerFavoritos([FromRoute] Guid usuarioId)
        {
            var favoritos = await _favoritoFlujo.ObtenerFavoritos(usuarioId);

            if (favoritos == null || !favoritos.Any())
                return NoContent();

            return Ok(favoritos);
        }

        // GET api/favorito/{favoritoId}
        [HttpGet("{favoritoId}")]
        public async Task<IActionResult> ObtenerFavoritoPorId([FromRoute] Guid favoritoId)
        {
            var favorito = await _favoritoFlujo.ObtenerFavoritoPorId(favoritoId);

            if (favorito == null)
                return NotFound();

            return Ok(favorito);
        }
        // GET api/favorito
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosFavoritos()
        {
            try
            {
                var favoritos = await _favoritoFlujo.ObtenerTodosFavoritos();

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

        private async Task<bool> VerificarFavoritoExiste(Guid favoritoId)
        {
            var favorito = await _favoritoFlujo.ObtenerFavoritoPorId(favoritoId);
            return favorito != null;
        }

        #endregion
    }
}
