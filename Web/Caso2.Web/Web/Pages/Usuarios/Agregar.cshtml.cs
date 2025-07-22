using System.Net;
using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Usuarios
{
    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public UsuarioRequest usuario{ get; set; }

        public async Task <IActionResult> OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarUsuario");

            using var cliente = new HttpClient();

            usuario.ID = Guid.NewGuid(); // Asignar nuevo ID si es necesario

            var respuesta = await cliente.PostAsJsonAsync(endpoint, usuario);

            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("/Usuarios/Index");
            }

            ModelState.AddModelError(string.Empty, "Error al guardar el Usuario.");
            return Page();
        }
    }
}
