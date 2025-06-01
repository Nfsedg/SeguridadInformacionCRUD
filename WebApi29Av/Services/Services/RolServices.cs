using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi29Av.Context;
using WebApi29Av.Services.IServices;

namespace WebApi29Av.Services.Services
{
    public class RolServices : IRolServices
    {
        private readonly ApplicationDBContext _context;

        public RolServices(ApplicationDBContext context)
        {
            _context = context;
        }

        // Lista de roles
        public async Task<Response<List<Rol>>> ObenerRoles()
        {
            try
            {
                var roles = await _context.Roles.ToListAsync();
                return new Response<List<Rol>>(roles, true, "Roles obtenidos correctamente.");
            }
            catch (Exception ex)
            {
                return new Response<List<Rol>>(null, false, $"Ocurrió un error al obtener los roles: {ex.Message}");
            }
        }

        public async Task<Response<Rol>> ById(int id)
        {
            if (id <= 0)
            {
                return new Response<Rol>(null, false, "El ID debe ser mayor que cero.");
            }

            try
            {
                var rol = await _context.Roles.FirstOrDefaultAsync(x => x.PkRol == id);

                if (rol == null)
                    return new Response<Rol>(null, false, $"No se encontró el rol con ID {id}.");

                return new Response<Rol>(rol, true, "Rol encontrado.");
            }
            catch (Exception ex)
            {
                return new Response<Rol>(null, false, $"Ocurrió un error al buscar el rol: {ex.Message}");
            }
        }

        public async Task<Response<Rol>> Crear(RolResponse request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre))
                return new Response<Rol>(null, false, "El nombre del rol es obligatorio.");

            try
            {
                var rol = new Rol
                {
                    Nombre = request.Nombre
                };

                _context.Roles.Add(rol);
                await _context.SaveChangesAsync();

                return new Response<Rol>(rol, true, "Rol creado correctamente.");
            }
            catch (Exception ex)
            {
                return new Response<Rol>(null, false, $"Error al crear el rol: {ex.Message}");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0) return false;

            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return false;
            }

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Response<Rol>> Update(int id, RolResponse request)
        {
            if (id <= 0)
                return new Response<Rol>(null, false, "El ID debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(request.Nombre))
                return new Response<Rol>(null, false, "El nombre del rol es obligatorio.");

            try
            {
                var existingRol = await _context.Roles.FirstOrDefaultAsync(r => r.PkRol == id);

                if (existingRol == null)
                    return new Response<Rol>(null, false, $"No se encontró el rol con ID {id}.");

                existingRol.Nombre = request.Nombre;

                _context.Roles.Update(existingRol);
                await _context.SaveChangesAsync();

                return new Response<Rol>(existingRol, true, "Rol actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return new Response<Rol>(null, false, $"Error al actualizar el rol: {ex.Message}");
            }
        }
    }
}