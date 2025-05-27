using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi29Av.Services.IServices;

namespace WebApi29Av.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUsuarioServices _usuarioServices;

        public UserController(IUsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }

        /// <summary>
        /// Obtiene todos los usuarios registrados.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _usuarioServices.ObtenerUsuarios();
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario</param>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok(await _usuarioServices.ById(id));
        }

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="request">Datos del usuario</param>
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UsuarioResponse request)
        {
            return Ok(await _usuarioServices.Crear(request));
        }

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _usuarioServices.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Usuario no encontrado." });
            }

            return Ok(new { message = "Usuario eliminado correctamente." });
        }

        /// <summary>
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        /// <param name="id">ID del usuario a actualizar</param>
        /// <param name="request">Datos actualizados</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioResponse request)
        {
            var result = await _usuarioServices.Update(id, request);

            if (result == null)
                return NotFound(new { message = "Usuario no encontrado." });

            return Ok(result);
        }
    }
}
