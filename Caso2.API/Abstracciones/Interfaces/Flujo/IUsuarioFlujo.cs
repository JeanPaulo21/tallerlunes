using Abstracciones.Modelos;


namespace Abstracciones.Interfaces.Flujo
{
    public interface IUsuarioFlujo
    {
        Task<Guid> AgregarUsuario(UsuarioRequest usuario);
        Task<Guid> EditarUsuario(Guid id, UsuarioRequest usuario);
        Task<Guid> EliminarUsuario(Guid id);
        Task<UsuarioResponse> ObtenerUsuarioPorId(Guid id);
        Task<IEnumerable<UsuarioResponse>> ObtenerUsuarios();
    }
}
