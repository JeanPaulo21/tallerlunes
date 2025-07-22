using Abstracciones.Modelos;


namespace Abstracciones.Interfaces.DA
{
    public interface IUsuarioDA
    {
        Task<Guid> AgregarUsuario(UsuarioRequest usuario);
        Task<Guid> EditarUsuario(Guid id, UsuarioRequest usuario);
        Task<Guid> EliminarUsuario(Guid id);
        Task<UsuarioResponse> ObtenerUsuarioPorId(Guid id);
        Task<IEnumerable<UsuarioResponse>> ObtenerUsuarios();
    }
}
