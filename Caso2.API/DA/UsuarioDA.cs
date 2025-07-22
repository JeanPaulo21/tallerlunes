using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Microsoft.Data.SqlClient;
using Dapper;


namespace DA
{
    public class UsuarioDA : IUsuarioDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public UsuarioDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerReposiorio();
        }

        public async Task<Guid> AgregarUsuario(UsuarioRequest usuario)
        {
            string sp = "SP_AgregarUsuario";
            var idNuevo = Guid.NewGuid();

            await _sqlConnection.ExecuteAsync(sp, new
            {
                ID = idNuevo,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo
            }, commandType: System.Data.CommandType.StoredProcedure);

            return idNuevo;
        }

        public async Task<Guid> EditarUsuario(Guid id, UsuarioRequest usuario)
        {
            await VerificarUsuarioExiste(id);

            string sp = "SP_EditarUsuario";

            await _sqlConnection.ExecuteAsync(sp, new
            {
                ID = id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo
            }, commandType: System.Data.CommandType.StoredProcedure);

            return id;
        }

        public async Task<Guid> EliminarUsuario(Guid id)
        {
            await VerificarUsuarioExiste(id);

            string sp = "SP_EliminarUsuario";

            await _sqlConnection.ExecuteAsync(sp, new { ID = id }, commandType: System.Data.CommandType.StoredProcedure);

            return id;
        }

        public async Task<UsuarioResponse> ObtenerUsuarioPorId(Guid id)
        {
            string sp = "SP_ObtenerUsuarioPorId";
            var resultado = await _sqlConnection.QueryAsync<UsuarioResponse>(sp, new { ID = id }, commandType: System.Data.CommandType.StoredProcedure);
            return resultado.FirstOrDefault();
        }

        public async Task<IEnumerable<UsuarioResponse>> ObtenerUsuarios()
        {
            string sp = "SP_ObtenerUsuarios";
            var resultado = await _sqlConnection.QueryAsync<UsuarioResponse>(sp, commandType: System.Data.CommandType.StoredProcedure);
            return resultado;
        }

        private async Task VerificarUsuarioExiste(Guid id)
        {
            var usuario = await ObtenerUsuarioPorId(id);
            if (usuario == null)
                throw new Exception("No se encontró el usuario");
        }
    }
}
