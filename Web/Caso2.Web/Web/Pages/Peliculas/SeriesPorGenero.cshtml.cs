using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios.TMDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

public class SeriesPorGenero : PageModel
{
    private readonly IConfiguracion _configuracion;

    public SeriesPorGenero(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    [BindProperty]
    public int GeneroSeleccionado { get; set; }

    public List<GeneroTMDB> Generos { get; set; } = new();

    public List<SerieTMDB> Series { get; set; } = new();

    public async Task OnGetAsync()
    {
        await CargarGenerosAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await CargarGenerosAsync();

        if (GeneroSeleccionado > 0)
        {
            await CargarSeriesPorGeneroAsync(GeneroSeleccionado);
        }

        return Page();
    }

    private async Task CargarGenerosAsync()
    {
        var url = _configuracion.ObtenerMetodo("ApiEndPointsTMDB", "ObtenerGenerosSeries");
        var token = _configuracion.ObtenerValor("TMDBToken");

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var stream = await response.Content.ReadAsStreamAsync();

        Generos = await JsonSerializer.DeserializeAsync<List<GeneroTMDB>>(stream, options);
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
