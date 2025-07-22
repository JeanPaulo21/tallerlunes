using System.Net.Http;
using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Favoritos
{
    public class EliminarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EliminarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public FavoritoResponse Favorito { get; set; } = default!;

        public async Task<IActionResult> OnGet(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerFavoritoPorId");

            using var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(string.Format(endpoint, id));

            if (!respuesta.IsSuccessStatusCode)
                return NotFound();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var favoritoRespuesta = JsonSerializer.Deserialize<FavoritoResponse>(resultado, opciones);
            if (favoritoRespuesta == null)
                return NotFound();

            Favorito = favoritoRespuesta;

            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarFavorito");

            using var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Delete, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
                return RedirectToPage("/Favoritos/Index");

            ModelState.AddModelError(string.Empty, "Error al eliminar el favorito.");
            return Page();
        }
    }
}
