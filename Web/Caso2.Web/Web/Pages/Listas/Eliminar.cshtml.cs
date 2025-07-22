using System.Net.Http;
using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Listas
{
    public class EliminarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EliminarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ListaVisualizacionResponse Lista { get; set; } = default!;

        public async Task<IActionResult> OnGet(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerItemPorId");

            using var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(string.Format(endpoint, id));

            if (!respuesta.IsSuccessStatusCode)
                return NotFound();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var listaRespuesta = JsonSerializer.Deserialize<ListaVisualizacionResponse>(resultado, opciones);
            if (listaRespuesta == null)
                return NotFound();

            Lista = listaRespuesta;

            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarItem");

            using var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Delete, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
                return RedirectToPage("/Listas/Index");

            ModelState.AddModelError(string.Empty, "Error al eliminar la lista visualización.");
            return Page();
        }
    }
}
