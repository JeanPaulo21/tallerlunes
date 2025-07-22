using Abstracciones.Modelos;
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;

namespace Flujo
{
    public class UsuarioFlujo : IUsuarioFlujo
    {
        private readonly IUsuarioDA _usuarioDA;

        public UsuarioFlujo(IUsuarioDA usuarioDA)
        {
            _usuarioDA = usuarioDA;
        }

        public async Task<Guid> AgregarUsuario(UsuarioRequest usuario)
        {
            return await _usuarioDA.AgregarUsuario(usuario);
        }

        public async Task<Guid> EditarUsuario(Guid id, UsuarioRequest usuario)
        {
            return await _usuarioDA.EditarUsuario(id, usuario);
        }

        public async Task<Guid> EliminarUsuario(Guid id)
        {
            return await _usuarioDA.EliminarUsuario(id);
        }

        public async Task<UsuarioResponse> ObtenerUsuarioPorId(Guid id)
        {
            return await _usuarioDA.ObtenerUsuarioPorId(id);
        }

        public async Task<IEnumerable<UsuarioResponse>> ObtenerUsuarios()
        {
            return await _usuarioDA.ObtenerUsuarios();
        }
    }
}
