using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.TMDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reglas
{
    public class TMDBReglas : ITMDBReglas
    {
        private readonly ITMDBServicio _tmdbServicio;

        public TMDBReglas(ITMDBServicio tmdbServicio)
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
            if (idGenero <= 0)
            {
                
                throw new System.ArgumentException("El id del género debe ser mayor a cero.");
            }

            return await _tmdbServicio.ObtenerPeliculasPorGenero(idGenero);
        }

        public async Task<SerieTMDBRespuesta> ObtenerSeriesPorGenero(int idGenero)
        {
            if (idGenero <= 0)
            {
                throw new System.ArgumentException("El id del género debe ser mayor a cero.");
            }

            return await _tmdbServicio.ObtenerSeriesPorGenero(idGenero);
        }
    }
}
