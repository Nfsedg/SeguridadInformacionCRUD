using Domain.Entities;

namespace WebApi29Av.Services.IServices
{
    public interface IUsuarioServices
    {
        // Obtener todos los usuarios
        Task<Response<List<Usuario>>> ObtenerUsuarios();

        // Obtener un usuario por ID
        Task<Response<Usuario>> ById(int id);

        // Crear un nuevo usuario
        Task<Response<Usuario>> Crear(UsuarioResponse request);

        // Eliminar un usuario por ID
        Task<bool> DeleteUserAsync(int id);
        Task<Response<Usuario>> Update(int id, UsuarioResponse request);

    }
}
