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
        //Lista de usuarios
        public async Task<Response<List<Rol>>> ObenerRoles()
        {
            try
            {

                List<Rol> response = await _context.Roles.ToListAsync();

                return new Response<List<Rol>>(response);

            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }
        public async Task<Response<Rol>> ById(int id)
        {
            try
            {
                Rol rol = await _context.Roles.FirstOrDefaultAsync(x => x.PkRol == id);

                return new Response<Rol>(rol);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Response<Rol>> Crear(RolResponse request)
        {
            try
            {
                Rol rol = new Rol()
                {
                    Nombre = request.Nombre
                };

                _context.Roles.Add(rol);
                await _context.SaveChangesAsync();

                return new Response<Rol>(rol);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return false; // Usuario no encontrado
            }

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Response<Rol>> Update(int id, RolResponse request)
        {
            try
            {
                var existingRol = await _context.Roles.FirstOrDefaultAsync(r => r.PkRol == id);

                if (existingRol == null)
                {
                    return new Response<Rol>("Rol no encontrado.");
                }

                existingRol.Nombre = request.Nombre;

                _context.Roles.Update(existingRol);
                await _context.SaveChangesAsync();

                return new Response<Rol>(existingRol);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el rol: " + ex.Message);
            }
        }
    }
}
