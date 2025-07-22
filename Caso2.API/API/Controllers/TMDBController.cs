using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TMDBController : ControllerBase, ITMDBController
    {
        private readonly ITMDBFlujo _tmdbFlujo;

        public TMDBController(ITMDBFlujo tmdbFlujo)
        {
            _tmdbFlujo = tmdbFlujo;
        }
        #region"Operaciones"
        [HttpGet("peliculas/generos")]
        public async Task<IActionResult> ObtenerGenerosPeliculas()
        {
            var resultado = await _tmdbFlujo.ObtenerGenerosPeliculas();
            if (resultado == null || !resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("series/generos")]
        public async Task<IActionResult> ObtenerGenerosSeries()
        {
            var resultado = await _tmdbFlujo.ObtenerGenerosSeries();
            if (resultado == null || !resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("peliculas/genero/{idGenero}")]
        public async Task<IActionResult> ObtenerPeliculasPorGenero([FromRoute] int idGenero)
        {
            var peliculas = await _tmdbFlujo.ObtenerPeliculasPorGenero(idGenero);
            if (peliculas == null || peliculas.Results == null || peliculas.Results.Count == 0)
                return NoContent();
            return Ok(peliculas);
        }

        [HttpGet("series/por-genero/{idGenero}")]
        public async Task<IActionResult> ObtenerSeriesPorGenero([FromRoute] int idGenero)
        {
            var resultado = await _tmdbFlujo.ObtenerSeriesPorGenero(idGenero);
            if (resultado == null || resultado.Results == null || !resultado.Results.Any())
                return NoContent();
            return Ok(resultado);
        }
    }
    #endregion
}
