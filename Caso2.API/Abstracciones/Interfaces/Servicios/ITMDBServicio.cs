using Abstracciones.Modelos.Servicios.TMDB;

namespace Abstracciones.Interfaces.Servicios
{
    public interface ITMDBServicio
    {
        Task<List<GeneroTMDB>> ObtenerGenerosPeliculas();
        Task<List<GeneroTMDB>> ObtenerGenerosSeries();
        Task<PeliculasTMDBRespuesta> ObtenerPeliculasPorGenero(int idGenero);
        Task<SerieTMDBRespuesta> ObtenerSeriesPorGenero(int idGenero);
    }
}
