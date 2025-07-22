using System.Net.Http.Headers;
using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Servicios.TMDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Favoritos
{
    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public FavoritoRequest Favorito { get; set; } = new FavoritoRequest();

        public List<GeneroTMDB> GenerosPeliculas { get; set; } = new();
        public List<GeneroTMDB> GenerosSeries { get; set; } = new();

        public List<PeliculaTMDB> Peliculas { get; set; } = new List<PeliculaTMDB>();
        public List<SerieTMDB> Series { get; set; } = new List<SerieTMDB>();

        public SelectList Usuarios { get; set; } = default!;  

        [BindProperty]
        public int GeneroSeleccionado { get; set; }

        [BindProperty]
        public Guid UsuarioSeleccionado { get; set; } 

        public string ErrorMessage { get; set; } = "";

        public async Task OnGetAsync()
        {
            await CargarGenerosPeliculasAsync();
            await CargarGenerosSeriesAsync();
            await CargarUsuariosAsync();
        }

        public async Task<IActionResult> OnPostBuscarAsync()
        {
            await CargarGenerosPeliculasAsync();
            await CargarGenerosSeriesAsync();
            await CargarUsuariosAsync();

            if (GeneroSeleccionado > 0 && !string.IsNullOrEmpty(Favorito.Tipo))
            {
                if (Favorito.Tipo == "Pelicula")
                {
                    await CargarPeliculasPorGeneroAsync(GeneroSeleccionado);
                    Series.Clear();
                }
                else if (Favorito.Tipo == "Serie")
                {
                    await CargarSeriesPorGeneroAsync(GeneroSeleccionado);
                    Peliculas.Clear();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostGuardarAsync()
        {
            // Cargar datos para mantener vista consistente en caso de error
            await CargarGenerosPeliculasAsync();
            await CargarGenerosSeriesAsync();
            await CargarUsuariosAsync();

            // Validaciones básicas
            if (!ModelState.IsValid || Favorito.TMDB_ID == 0 || string.IsNullOrEmpty(Favorito.Tipo))
            {
                if (Favorito.TMDB_ID == 0 || string.IsNullOrEmpty(Favorito.Tipo))
                {
                    ErrorMessage = "Debe seleccionar una película o serie válida.";
                }

                if (GeneroSeleccionado > 0 && !string.IsNullOrEmpty(Favorito.Tipo))
                {
                    if (Favorito.Tipo == "Pelicula")
                        await CargarPeliculasPorGeneroAsync(GeneroSeleccionado);
                    else if (Favorito.Tipo == "Serie")
                        await CargarSeriesPorGeneroAsync(GeneroSeleccionado);
                }

                return Page();
            }

            // Asignar el usuario seleccionado al favorito
            Favorito.UsuarioID = UsuarioSeleccionado;

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarFavorito");
            using var cliente = new HttpClient();

            var respuesta = await cliente.PostAsJsonAsync(endpoint, Favorito);

            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("/Favoritos/Index");
            }
            else
            {
                ErrorMessage = "Error al guardar el favorito.";
                return Page();
            }
        }

        private async Task CargarUsuariosAsync()
        {
            string url = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerUsuarios");
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var json = await response.Content.ReadAsStringAsync();

            var usuarios = JsonSerializer.Deserialize<List<UsuarioResponse>>(json, options) ?? new List<UsuarioResponse>();

            Usuarios = new SelectList(usuarios, "ID", "Nombre");
        }

        private async Task CargarGenerosPeliculasAsync()
        {
            var urlGeneros = _configuracion.ObtenerMetodo("ApiEndPointsTMDB", "ObtenerGenerosPeliculas");
            var token = _configuracion.ObtenerValor("TMDBToken");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(urlGeneros);
            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var stream = await response.Content.ReadAsStreamAsync();

            GenerosPeliculas = await JsonSerializer.DeserializeAsync<List<GeneroTMDB>>(stream, options);
        }

        private async Task CargarGenerosSeriesAsync()
        {
            var url = _configuracion.ObtenerMetodo("ApiEndPointsTMDB", "ObtenerGenerosSeries");
            var token = _configuracion.ObtenerValor("TMDBToken");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var stream = await response.Content.ReadAsStreamAsync();

            GenerosSeries = await JsonSerializer.DeserializeAsync<List<GeneroTMDB>>(stream, options);
        }

        private async Task CargarPeliculasPorGeneroAsync(int generoId)
        {
            var url = _configuracion.ObtenerMetodo("ApiEndPointsTMDB", "ObtenerPeliculasPorGenero");
            var token = _configuracion.ObtenerValor("TMDBToken");

            url = string.Format(url, generoId);

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var stream = await response.Content.ReadAsStreamAsync();

            var resultado = await JsonSerializer.DeserializeAsync<PeliculasTMDBRespuesta>(stream, options);
            Peliculas = resultado?.Results ?? new List<PeliculaTMDB>();
        }

        private async Task CargarSeriesPorGeneroAsync(int generoId)
        {
            var url = _configuracion.ObtenerMetodo("ApiEndPointsTMDB", "ObtenerSeriesPorGenero");
            var token = _configuracion.ObtenerValor("TMDBToken");

            url = string.Format(url, generoId);

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var stream = await response.Content.ReadAsStreamAsync();

            var resultado = await JsonSerializer.DeserializeAsync<SerieTMDBRespuesta>(stream, options);
            Series = resultado?.Results ?? new List<SerieTMDB>();
        }
    }
}
