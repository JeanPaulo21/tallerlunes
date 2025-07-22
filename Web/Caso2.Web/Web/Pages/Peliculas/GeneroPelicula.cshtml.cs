using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios.TMDB;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

public class GenerosPeliculasModel : PageModel
{
    private readonly IConfiguracion _configuracion;

    public List<GeneroTMDB> GeneroPelicula { get; set; } = new();

    public GenerosPeliculasModel(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    public async Task OnGetAsync()
    {
        var url = _configuracion.ObtenerMetodo("ApiEndPointsTMDB", "ObtenerGenerosPeliculas");
        var token = _configuracion.ObtenerValor("TMDBToken");

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var contentStream = await response.Content.ReadAsStreamAsync();

        GeneroPelicula = await JsonSerializer.DeserializeAsync<List<GeneroTMDB>>(contentStream, options);
    }
}
