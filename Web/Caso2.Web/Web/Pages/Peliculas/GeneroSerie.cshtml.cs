using System.Net.Http.Headers;
using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios.TMDB;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class GenerosSeriesModel : PageModel
{
    private readonly IConfiguracion _configuracion;

    public List<GeneroTMDB> GeneroSerie { get; set; } = new();

    public GenerosSeriesModel(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    public async Task OnGetAsync()
    {
        var url = _configuracion.ObtenerMetodo("ApiEndPointsTMDB", "ObtenerGenerosSeries");
        var token = _configuracion.ObtenerValor("TMDBToken");

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var contentStream = await response.Content.ReadAsStreamAsync();

        
        GeneroSerie = await JsonSerializer.DeserializeAsync<List<GeneroTMDB>>(contentStream, options);
    }
}

