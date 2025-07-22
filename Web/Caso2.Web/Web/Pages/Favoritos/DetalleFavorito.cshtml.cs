using System.Net;
using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Favoritos
{
    public class DetalleFavoritoModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public FavoritoResponse Favorito { get; set; } = default!;

        public DetalleFavoritoModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerFavoritoPorId");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

            var respuesta = await cliente.SendAsync(solicitud);
            if (respuesta.StatusCode == HttpStatusCode.NotFound)
                return NotFound();

            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Favorito = JsonSerializer.Deserialize<FavoritoResponse>(resultado, opciones);

            if (Favorito == null)
                return NotFound();

            return Page();
        }
    }
}
