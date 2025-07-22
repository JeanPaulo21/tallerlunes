using Abstracciones.Interfaces.DA;  
using Microsoft.Data.SqlClient; 
using Microsoft.Extensions.Configuration;   

namespace DA.Repositorios
{
    public class RepositorioDapper: IRepositorioDapper
    {
        private readonly IConfiguration _configuracion;
        private readonly SqlConnection _conexionBaseDatos;

        public RepositorioDapper(IConfiguration _configuracion)
        {
            _configuracion = _configuracion;
            _conexionBaseDatos=new SqlConnection(_configuracion.GetConnectionString("BD"));
        }

        public SqlConnection ObtenerReposiorio()
        {
            return _conexionBaseDatos;
        }
    }
}
