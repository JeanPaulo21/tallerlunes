using System.Net;
using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Listas
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public IList<ListaVisualizacionResponse> Listas{ get; set; } = default!;

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerVisualizaciones");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                Listas = JsonSerializer.Deserialize<List<ListaVisualizacionResponse>>(resultado, opciones) ?? new List<ListaVisualizacionResponse>();
            }
        }
    }
}
