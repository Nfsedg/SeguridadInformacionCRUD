using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi29Av.Context;
using WebApi29Av.Services.IServices;

namespace WebApi29Av.Services.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly ApplicationDBContext _context;
        public UsuarioServices(ApplicationDBContext context) 
        {
            _context = context;
        }
        //Lista de usuarios
        public async Task<Response<List<Usuario>>> ObtenerUsuarios()
        {
            try
            {

                List<Usuario> response = await _context.Usuarios.ToListAsync();

                return new Response<List<Usuario>>(response);

            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }
        public async Task<Response<Usuario>> ById(int id)
        {
            try
            {
                Usuario user = await _context.Usuarios.FirstOrDefaultAsync(x => x.PkUsuario == id);

                return new Response<Usuario>(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Response<Usuario>> Crear(UsuarioResponse request)
        {
            try
            {
                Usuario usuario = new Usuario()
                {
                    Nombre = request.Nombre,
                    UserName = request.User,
                    Password = request.Password,
                    FkRol = request.FkRol
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return new Response<Usuario>(usuario);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null)
            {
                return false; // Usuario no encontrado
            }

            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Response<Usuario>> Update(int id, UsuarioResponse request)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.PkUsuario == id);

                if (usuario == null)
                {
                    return new Response<Usuario>("Usuario no encontrado.");
                }

                usuario.Nombre = request.Nombre;
                usuario.UserName = request.User;
                usuario.Password = request.Password;
                usuario.FkRol = request.FkRol;

                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();

                return new Response<Usuario>(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario: " + ex.Message);
            }
        }
    }
}
