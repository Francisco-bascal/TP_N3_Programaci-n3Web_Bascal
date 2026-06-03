using APP_PRUEBA_1.Models;
using APP_PRUEBA_1.Servicios;
using APP_PRUEBA_1.Servicios.Validation;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APP_PRUEBA_1.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioService _servicio;
        public UsuariosController(IUsuarioService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                return View(await _servicio.GetUsuariosAsync());
            }
            catch (Exception ex)
            {
                TempData["Errores"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetUsuariosFiltrados(string busqueda) 
        {
            try
            {
                var resultado = await _servicio.GetUsuarioByNameOrLastNameAsync(busqueda);
                if (!resultado.IsValid) 
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return RedirectToAction("GetUsuarios");
                }
                return View("GetUsuarios", resultado.Value);
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return RedirectToAction("GetUsuarios");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Detalles(int id)
        {
            try
            {
                var resultado = await _servicio.GetUsuarioByIdAsync(id);
                if (!resultado.IsValid)
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return RedirectToAction("GetUsuarios");
                }
                return View(resultado.Value);
            }
            catch (Exception ex)
            {
                TempData["Errores"] = ex.Message;
                return RedirectToAction("GetUsuarios");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            try
            {
                var resultado = await _servicio.PostUsuarioAsync(usuario);
                if (!resultado.IsValid)
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return View(usuario);
                }
                TempData["Exito"] = "Usuario creado correctamente";
                return RedirectToAction("GetUsuarios");
            }
            catch (Exception ex)
            {
                TempData["Errores"] = ex.Message;
                return RedirectToAction("GetUsuarios");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var resultado = await _servicio.GetUsuarioByIdAsync(id);
                if (!resultado.IsValid) 
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return RedirectToAction("GetUsuarios");
                }
                return View(resultado.Value);
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return RedirectToAction("GetUsuarios");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Usuario usuario)
        {
            Result<Usuario> resultado;
            try
            {
                resultado = await _servicio.PutUsuarioAsync(usuario);
                if (!resultado.IsValid)
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return View(usuario);
                }
                TempData["Exito"] = "Usuario editado correctamente";
                return RedirectToAction("GetUsuarios");
            }
            catch (Exception ex)
            {
                TempData["Errores"] = ex.Message;
                return RedirectToAction("GetUsuarios");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id) 
        {
            try
            {
                var resultado = await _servicio.DeleteUsuarioByIdAsync(id);
                if (!resultado.IsValid) 
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return RedirectToAction("GetUsuarios");
                }
                TempData["Exito"] = "Usuario eliminado correctamente";
                return RedirectToAction("GetUsuarios");
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return RedirectToAction("GetUsuarios");
            }
        }
    }
}