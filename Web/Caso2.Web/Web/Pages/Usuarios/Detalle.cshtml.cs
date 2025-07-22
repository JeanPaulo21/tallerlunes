using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Usuarios
{
    public class DetalleModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public UsuarioResponse usuario { get; set; } = default!;

        public DetalleModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                // Redirigir si no hay ID
                Response.Redirect("/Usuarios/Index");
                return;
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerUsuarioPorId");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            usuario = JsonSerializer.Deserialize<UsuarioResponse>(resultado, opciones)
                      ?? throw new InvalidOperationException("No se pudo deserializar el usuario.");
        }
    }
}
