using System.Net.Http.Json;
using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Listas
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ListaVisualizacionRequest Lista { get; set; } = default!;

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

            var listaRespuesta = JsonSerializer.Deserialize<ListaVisualizacionRequest>(resultado, opciones);
            if (listaRespuesta == null)
                return NotFound();

            // Solo llenar los campos editables + los necesarios para mostrar
            Lista = new ListaVisualizacionRequest
            {
                ID = listaRespuesta.ID,
                UsuarioID = listaRespuesta.UsuarioID,
                TMDB_ID = listaRespuesta.TMDB_ID,
                Tipo = listaRespuesta.Tipo,
                Prioridad = listaRespuesta.Prioridad,
                Comentario = listaRespuesta.Comentario                
            };

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarItem");

            using var cliente = new HttpClient();
            var respuesta = await cliente.PutAsJsonAsync(string.Format(endpoint, Lista.ID), Lista);

            if (respuesta.IsSuccessStatusCode)
                return RedirectToPage("/Listas/Index");

            ModelState.AddModelError(string.Empty, "Error al actualizar el lista.");
            return Page();
        }
    }
}
