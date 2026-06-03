using APP_PRUEBA_1.Models;
using Microsoft.EntityFrameworkCore;

namespace APP_PRUEBA_1.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly RRHH2026_Db_Context _contexto;
        public UsuarioRepository(RRHH2026_Db_Context contexto)
        {
            _contexto = contexto;
        }
        public async Task<ICollection<Usuario>> GetUsuariosAsync() 
        {
            return await _contexto.Usuarios.ToListAsync();
        }
        public async Task<Usuario> GetUsuarioByIdAsync(int id) 
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario.Equals(id));
        }
        public async Task<Usuario?> GetUsuarioByCredencialesAsync(string nombreUsuario, string contraseña) 
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Nombre.Equals(nombreUsuario) && u.Pass.Equals(contraseña));
        }
        public async Task PostUsuarioAsync(Usuario usuario) 
        {
            await _contexto.Usuarios.AddAsync(usuario);
            await _contexto.SaveChangesAsync();
        }
        public async Task PutUsuarioAsync(Usuario usuario) 
        {
            var existe = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario.Equals(usuario.IdUsuario));

            existe.Nombre = usuario.Nombre;
            existe.Apellido = usuario.Apellido;
            existe.Rol = usuario.Rol;
            existe.Pass = usuario.Pass;

            await _contexto.SaveChangesAsync();
        }
        public async Task DeleteUsuarioByIdAsync(int id) 
        {
            var existe = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario.Equals(id));
            _contexto.Usuarios.Remove(existe);
            await _contexto.SaveChangesAsync();
        }
    }
}