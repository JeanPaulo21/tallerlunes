using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Listas
{
    public class DetalleUsuarioModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public List<ListaVisualizacionResponse> Listas { get; set; } = new();

        public List<SelectListItem> ListaUsuarios { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public Guid UsuarioSeleccionado { get; set; }

        public bool NoHayListas { get; set; } = false;

        public DetalleUsuarioModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGetAsync()
        {
            await CargarUsuariosAsync();

            if (UsuarioSeleccionado != Guid.Empty)
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerLista");
                using var cliente = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, UsuarioSeleccionado));

                var response = await cliente.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    if (!string.IsNullOrWhiteSpace(contenido))
                    {
                        Listas = JsonSerializer.Deserialize<List<ListaVisualizacionResponse>>(contenido, opciones) ?? new List<ListaVisualizacionResponse>();
                    }
                    else
                    {
                        Listas = new List<ListaVisualizacionResponse>();
                    }
                }
                else
                {
                    // En caso de error, asignamos lista vacía o puedes manejarlo según tu lógica
                    Listas = new List<ListaVisualizacionResponse>();
                }

                NoHayListas = Listas.Count == 0;
            }
            else
            {
                NoHayListas = false;
            }
        }


        private async Task CargarUsuariosAsync()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerUsuarios");
            var cliente = new HttpClient();
            var response = await cliente.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var contenido = await response.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var usuarios = JsonSerializer.Deserialize<List<UsuarioResponse>>(contenido, opciones) ?? new();

            ListaUsuarios = usuarios.Select(u => new SelectListItem
            {
                Value = u.ID.ToString(),
                Text = u.Nombre
            }).ToList();
        }
    }
}
