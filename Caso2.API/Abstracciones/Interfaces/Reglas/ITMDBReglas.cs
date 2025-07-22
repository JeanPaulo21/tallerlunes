using Abstracciones.Modelos.Servicios.TMDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Reglas
{
    public interface ITMDBReglas
    {
        Task<List<GeneroTMDB>> ObtenerGenerosPeliculas();
        Task<List<GeneroTMDB>> ObtenerGenerosSeries();
        Task<PeliculasTMDBRespuesta> ObtenerPeliculasPorGenero(int idGenero);
        Task<SerieTMDBRespuesta> ObtenerSeriesPorGenero(int idGenero);
    }
}
