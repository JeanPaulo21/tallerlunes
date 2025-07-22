using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioFlujo _usuarioFlujo;

        public UsuarioController(IUsuarioFlujo usuarioFlujo)
        {
            _usuarioFlujo = usuarioFlujo;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var usuarios = await _usuarioFlujo.ObtenerUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuarioPorId(Guid id)
        {
            var usuario = await _usuarioFlujo.ObtenerUsuarioPorId(id);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarUsuario([FromBody] UsuarioRequest usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _usuarioFlujo.AgregarUsuario(usuario);
            return CreatedAtAction(nameof(ObtenerUsuarioPorId), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarUsuario(Guid id, [FromBody] UsuarioRequest usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var actualizadoId = await _usuarioFlujo.EditarUsuario(id, usuario);
            return Ok(new { id = actualizadoId });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(Guid id)
        {
            var eliminadoId = await _usuarioFlujo.EliminarUsuario(id);
            return Ok(new { id = eliminadoId });
        }
    }
}
