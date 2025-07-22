using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.TMDB;

namespace Flujo
{
    public class TMDBFlujo : ITMDBFlujo
    {
        private readonly ITMDBServicio _tmdbServicio;

        public TMDBFlujo(ITMDBServicio tmdbServicio)
        {
            _tmdbServicio = tmdbServicio;
        }

        public async Task<List<GeneroTMDB>> ObtenerGenerosPeliculas()
        {
            return await _tmdbServicio.ObtenerGenerosPeliculas();
        }

        public async Task<List<GeneroTMDB>> ObtenerGenerosSeries()
        {
            return await _tmdbServicio.ObtenerGenerosSeries();
        }

        public async Task<PeliculasTMDBRespuesta> ObtenerPeliculasPorGenero(int idGenero)
        {
            return await _tmdbServicio.ObtenerPeliculasPorGenero(idGenero);
        }

        public async Task<SerieTMDBRespuesta> ObtenerSeriesPorGenero(int idGenero)
        {
            return await _tmdbServicio.ObtenerSeriesPorGenero(idGenero);
        }
    }
}
