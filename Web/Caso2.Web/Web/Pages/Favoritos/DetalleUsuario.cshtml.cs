using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Favoritos
{
    public class DetalleUsuarioModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public List<FavoritoResponse> Favoritos { get; set; } = new();

        public List<SelectListItem> ListaUsuarios { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public Guid UsuarioSeleccionado { get; set; }

        public bool NoHayFavoritos { get; set; } = false;

        public DetalleUsuarioModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGetAsync()
        {
            await CargarUsuariosAsync();

            if (UsuarioSeleccionado != Guid.Empty)
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerFavoritos");
                using var cliente = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, UsuarioSeleccionado));

                var response = await cliente.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    if (!string.IsNullOrWhiteSpace(contenido))
                    {
                        Favoritos = JsonSerializer.Deserialize<List<FavoritoResponse>>(contenido, opciones) ?? new List<FavoritoResponse>();
                    }
                    else
                    {
                        Favoritos = new List<FavoritoResponse>();
                    }
                }
                else
                {                   
                    Favoritos = new List<FavoritoResponse>();
                }

                NoHayFavoritos = Favoritos.Count == 0;
            }
            else
            {
                NoHayFavoritos = false;
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
