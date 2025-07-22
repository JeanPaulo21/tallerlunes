using Abstracciones.Modelos.Servicios.TMDB;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface ITMDBController
    {
        Task<IActionResult> ObtenerGenerosPeliculas();
        Task<IActionResult> ObtenerGenerosSeries();

        Task<IActionResult> ObtenerPeliculasPorGenero(int idGenero);
        Task<IActionResult> ObtenerSeriesPorGenero(int idGenero);
    }
}
