
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.Extensions.Configuration;

namespace Reglas
{
    public class Configuracion : IConfiguracion
    {
        private readonly IConfiguration _configuration;

        public Configuracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //NUEVO OBTENER METODO
        public string ObtenerMetodo(string seccion, string nombre)
        {
            string? urlBase = ObtenerUrlBase(seccion)?.TrimEnd('/');
            var metodo = _configuration.GetSection(seccion)
                .Get<APIEndPoint>()?
                .Metodos?
                .FirstOrDefault(m => m.Nombre == nombre)?
                .Valor?
                .TrimStart('/');

            return $"{urlBase}/{metodo}";
        }

        private string? ObtenerUrlBase(string seccion)
        {
            return _configuration.GetSection(seccion).Get<APIEndPoint>()?.UrlBase;
        }

        public string ObtenerValor(string llave)
        {
            return _configuration.GetSection(llave).Value;
        }
    }
}
