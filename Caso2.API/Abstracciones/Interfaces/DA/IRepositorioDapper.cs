

using Microsoft.Data.SqlClient;

namespace Abstracciones.Interfaces.DA
{
    public interface IRepositorioDapper
    {
        // contrato para obtener una conexión a la base de datos
        SqlConnection ObtenerReposiorio();
    }
}
