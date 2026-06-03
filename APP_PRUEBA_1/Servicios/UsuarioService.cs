using APP_PRUEBA_1.Models;
using APP_PRUEBA_1.Repositorios;
using APP_PRUEBA_1.Servicios.Validation;

namespace APP_PRUEBA_1.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repositorio;
        public UsuarioService(IUsuarioRepository repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<ICollection<Usuario>> GetUsuariosAsync() 
        {
            return await _repositorio.GetUsuariosAsync();
        }
        public async Task<Result<Usuario>> GetUsuarioByIdAsync(int id) 
        {
            var existe = await _repositorio.GetUsuarioByIdAsync(id);

            if (existe == null) return Result<Usuario>.Failure($"El usuario con el id: {id} no existe");

            return Result<Usuario>.Success(existe);
        }
        public async Task<Result<Usuario>> GetUsuarioByCredencialesAsync(string nombreUsuario, string contraseña) 
        {
            var existe = await _repositorio.GetUsuarioByCredencialesAsync(nombreUsuario, contraseña);

            if (existe == null) return Result<Usuario>.Failure("El usuario con las credenciales ingresadas no existe");

            return Result<Usuario>.Success(existe);
        }
        public async Task<Result<Usuario>> PostUsuarioAsync(Usuario usuario) 
        {
            var existe = await _repositorio.GetUsuarioByCredencialesAsync(usuario.Nombre, usuario.Pass);
            
            if (existe != null) return Result<Usuario>.Failure("Ya existe un usuario con estas credenciales");
            await _repositorio.PostUsuarioAsync(usuario);
            return Result<Usuario>.Success(usuario);
        }
        public async Task<Result<Usuario>> PutUsuarioAsync(Usuario usuario) 
        {
            var existe = await _repositorio.GetUsuarioByIdAsync(usuario.IdUsuario);
            if (existe == null) return Result<Usuario>.Failure($"No existe el usuario con el id {usuario.IdUsuario}");

            var verificacionNombre = await _repositorio.GetUsuarioByNameAsync(usuario.Nombre);
            if (verificacionNombre != null && verificacionNombre.IdUsuario != usuario.IdUsuario) return Result<Usuario>.Failure("Ya existe un usuario con este nombre de usuario");

            await _repositorio.PutUsuarioAsync(usuario);
            return Result<Usuario>.Success(usuario);
        }

        public async Task<Result<Usuario>> DeleteUsuarioByIdAsync(int id) 
        {
            var existe = await _repositorio.GetUsuarioByIdAsync(id);
            if (existe == null) return Result<Usuario>.Failure($"No existe el usuario con el id {id}");

            await _repositorio.DeleteUsuarioByIdAsync(id);
            return Result<Usuario>.Success(existe);
        }
    }
}