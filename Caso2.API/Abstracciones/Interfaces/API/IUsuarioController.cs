using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface IUsuarioController
    {
        Task<IActionResult> ObtenerUsuarios();
        Task<IActionResult> ObtenerUsuarioPorId(Guid id);
        Task<IActionResult> AgregarUsuario(UsuarioRequest usuario);
        Task<IActionResult> EditarUsuario(Guid id, UsuarioRequest usuario);
        Task<IActionResult> EliminarUsuario(Guid id);
    }
}
