using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios.TMDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

public class PeliculasPorGenero : PageModel
{
    private readonly IConfiguracion _configuracion;

    public PeliculasPorGenero(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    // Lista para el dropdown de géneros
    [BindProperty]
    public int GeneroSeleccionado { get; set; }

    public List<GeneroTMDB> Generos { get; set; } = new();

    public List<PeliculaTMDB> Peliculas { get; set; } = new();

    public async Task OnGetAsync()
    {
        await CargarGenerosAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await CargarGenerosAsync();

        if (GeneroSeleccionado > 0)
        {
            await CargarPeliculasPorGeneroAsync(GeneroSeleccionado);
        }

        return Page();
    }

    private async Task CargarGenerosAsync()
    {
        var urlGeneros = _configuracion.ObtenerMetodo("ApiEndPointsTMDB", "ObtenerGenerosPeliculas");
        var token = _configuracion.ObtenerValor("TMDBToken");

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync(urlGeneros);
        response.EnsureSuccessStatusCode();

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var stream = await response.Content.ReadAsStreamAsync();

        Generos = await JsonSerializer.DeserializeAsync<List<GeneroTMDB>>(stream, options);
    }

    private async Task CargarPeliculasPorGeneroAsync(int generoId)
    {
        // Aquí ya obtienes la URL completa con base + método
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
}
