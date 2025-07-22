using System.Net.Http.Json;
using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Favoritos
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public FavoritoRequest Favorito { get; set; } = default!;

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

            // Solo llenar los campos editables + los necesarios para mostrar
            Favorito = new FavoritoRequest
            {
                ID = favoritoRespuesta.ID,
                UsuarioID = favoritoRespuesta.UsuarioID,
                TMDB_ID = favoritoRespuesta.TMDB_ID,
                Tipo = favoritoRespuesta.Tipo,
                Comentario = favoritoRespuesta.Comentario,
                Puntuacion = favoritoRespuesta.Puntuacion
            };

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarFavorito");

            using var cliente = new HttpClient();
            var respuesta = await cliente.PutAsJsonAsync(string.Format(endpoint, Favorito.ID), Favorito);

            if (respuesta.IsSuccessStatusCode)
                return RedirectToPage("./Index");

            ModelState.AddModelError(string.Empty, "Error al actualizar el favorito.");
            return Page();
        }
    }
}
