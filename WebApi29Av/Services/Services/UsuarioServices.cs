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

        // Obtener lista de usuarios
        public async Task<Response<List<Usuario>>> ObtenerUsuarios()
        {
            try
            {
                var usuarios = await _context.Usuarios.ToListAsync();

                if (usuarios == null || !usuarios.Any())
                    return new Response<List<Usuario>>(null, false, "No hay usuarios registrados.");

                return new Response<List<Usuario>>(usuarios);
            }
            catch (Exception ex)
            {
                return new Response<List<Usuario>>(null, false, $"Error al obtener usuarios: {ex.Message}");
            }
        }

        // Obtener usuario por ID
        public async Task<Response<Usuario>> ById(int id)
        {
            if (id <= 0)
                return new Response<Usuario>(null, false, "ID inválido.");

            try
            {
                var user = await _context.Usuarios.FirstOrDefaultAsync(x => x.PkUsuario == id);

                if (user == null)
                    return new Response<Usuario>(null, false, "Usuario no encontrado.");

                return new Response<Usuario>(user);
            }
            catch (Exception ex)
            {
                return new Response<Usuario>(null, false, $"Error al buscar el usuario: {ex.Message}");
            }
        }

        // Crear nuevo usuario
        public async Task<Response<Usuario>> Crear(UsuarioResponse request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre) || string.IsNullOrWhiteSpace(request.User) || string.IsNullOrWhiteSpace(request.Password))
            {
                return new Response<Usuario>(null, false, "Nombre, usuario y contraseña son obligatorios.");
            }

            try
            {
                var usuario = new Usuario
                {
                    Nombre = request.Nombre,
                    UserName = request.User,
                    Password = request.Password,
                    FkRol = request.FkRol
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return new Response<Usuario>(usuario, true, "Usuario creado exitosamente.");
            }
            catch (Exception ex)
            {
                return new Response<Usuario>(null, false, $"Error al crear usuario: {ex.Message}");
            }
        }

        // Eliminar usuario por ID
        public async Task<bool> DeleteUserAsync(int id)
        {
            if (id <= 0)
                return false;

            var user = await _context.Usuarios.FindAsync(id);
            if (user == null)
                return false;

            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // Actualizar usuario existente
        public async Task<Response<Usuario>> Update(int id, UsuarioResponse request)
        {
            if (id <= 0)
                return new Response<Usuario>(null, false, "ID inválido.");

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.PkUsuario == id);

                if (usuario == null)
                    return new Response<Usuario>(null, false, "Usuario no encontrado.");

                usuario.Nombre = request.Nombre;
                usuario.UserName = request.User;
                usuario.Password = request.Password;
                usuario.FkRol = request.FkRol;

                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();

                return new Response<Usuario>(usuario, true, "Usuario actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                return new Response<Usuario>(null, false, $"Error al actualizar usuario: {ex.Message}");
            }
        }
    }
}