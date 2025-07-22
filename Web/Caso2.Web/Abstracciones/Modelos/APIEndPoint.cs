using System.Collections.Generic;

namespace Abstracciones.Modelos
{
    public class APIEndPoint
    {
        public string UrlBase { get; set; }
        public List<MetodoEndPoint> Metodos { get; set; }
    }

    public class MetodoEndPoint
    {
        public string Nombre { get; set; }
        public string Valor { get; set; }
    }
}
