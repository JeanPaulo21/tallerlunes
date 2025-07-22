using Abstracciones.Modelos.Servicios.TMDB;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ITMDBFlujo
    {
        Task<List<GeneroTMDB>> ObtenerGenerosPeliculas();
        Task<List<GeneroTMDB>> ObtenerGenerosSeries();

        Task<PeliculasTMDBRespuesta> ObtenerPeliculasPorGenero(int idGenero);
        Task<SerieTMDBRespuesta> ObtenerSeriesPorGenero(int idGenero);
    }
}
