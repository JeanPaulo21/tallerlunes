using System.Net.Http;
using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.TMDB;

namespace Servicios
{
    public class TMDBServicio : ITMDBServicio
    {
        private readonly IConfiguracion _configuracion;
        private readonly IHttpClientFactory _httpClientFactory;

        public TMDBServicio(IConfiguracion configuracion, IHttpClientFactory httpClientFactory)
        {
            _configuracion = configuracion;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<GeneroTMDB>> ObtenerGenerosPeliculas()
        {
            string url = _configuracion.ObtenerMetodo("ApiEndPointsTMDB", "ObtenerGenerosPeliculas");
            return await ObtenerGeneros(url);
        }

        public async Task<List<GeneroTMDB>> ObtenerGenerosSeries()
        {
            string url = _configuracion.ObtenerMetodo("ApiEndPointsTMDB", "ObtenerGenerosSeries");
            return await ObtenerGeneros(url);
        }

        private async Task<List<GeneroTMDB>> ObtenerGeneros(string url)
        {
            var token = _configuracion.ObtenerValor("TMDBToken");

            var cliente = _httpClientFactory.CreateClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var respuesta = await cliente.GetAsync(url);
            respuesta.EnsureSuccessStatusCode();

            var contenido = await respuesta.Content.ReadAsStringAsync();

            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var resultado = JsonSerializer.Deserialize<GeneroTMDBRespuesta>(contenido, opciones);

            return resultado?.Genres ?? new List<GeneroTMDB>();
        }
        public async Task<PeliculasTMDBRespuesta> ObtenerPeliculasPorGenero(int idGenero)
        {
            var endpointPlantilla = _configuracion.ObtenerMetodo("ApiEndPointsTMDB", "ObtenerPeliculasPorGenero");
            var endpoint = string.Format(endpointPlantilla, idGenero);

            var cliente = _httpClientFactory.CreateClient("TMDB");
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _configuracion.ObtenerValor("TMDBToken"));

            var respuesta = await cliente.GetAsync(endpoint);
            respuesta.EnsureSuccessStatusCode();

            var contenido = await respuesta.Content.ReadAsStringAsync();

            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<PeliculasTMDBRespuesta>(contenido, opciones);
        }
        public async Task<SerieTMDBRespuesta> ObtenerSeriesPorGenero(int idGenero)
        {
            var endPointTemplate = _configuracion.ObtenerMetodo("ApiEndPointsTMDB", "ObtenerSeriesPorGenero");
            var endPoint = string.Format(endPointTemplate, idGenero);

            var cliente = _httpClientFactory.CreateClient("TMDB");
            cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {_configuracion.ObtenerValor("TMDBToken")}");

            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();

            var contenido = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<SerieTMDBRespuesta>(contenido, opciones);
        }
    }
}

