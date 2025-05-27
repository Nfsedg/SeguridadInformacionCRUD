using Domain.Entities;

namespace WebApi29Av.Services.IServices
{
    public interface IRolServices
    {
        // Obtener todos los roles
        Task<Response<List<Rol>>> ObenerRoles();

        // Obtener un rol por su ID
        Task<Response<Rol>> ById(int id);

        // Crear un nuevo rol
        Task<Response<Rol>> Crear(RolResponse request);

        // Actualizar un rol existente
        Task<Response<Rol>> Update(int id, RolResponse request);

        // Eliminar un rol por su ID
        Task<bool> DeleteAsync(int id);
    }
}
