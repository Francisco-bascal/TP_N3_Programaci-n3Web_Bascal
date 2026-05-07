using APP_PRUEBA_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using APP_PRUEBA_1.Servicios;
using APP_PRUEBA_1.Servicios.Validation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace APP_PRUEBA_1.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly IEmpleadoService _servicio;
        private readonly IDepartamentoService _servicioDepartamento;
        public EmpleadosController(IEmpleadoService servicio, IDepartamentoService servicioDepartamento)
        {
            _servicio = servicio;
            _servicioDepartamento = servicioDepartamento;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmpleadosAsync() 
        {
            try
            {
                var empleados = await _servicio.GetEmpleadosAsync();
                return View(empleados);
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return View();
            }
        }

        [HttpGet] 
        public async Task<IActionResult> GetEmpleadoByIdAsync(int id) 
        {
            try
            {
                var resultado = await _servicio.GetEmpleadoByIdAsync(id);

                if (!resultado.IsValid)
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors); //para mostrar los errores en un alert 
                    return RedirectToAction("GetEmpleados");
                }

                return View("Detalles", resultado.Value);
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return View("GetEmpleados");
            } 
        }

        [HttpGet]
        public async Task<IActionResult> GetEmpleadosFiltro(string busqueda) 
        {
            var resultado = await _servicio.GetEmpleadosByNombreOApellidoAsync(busqueda);

            if (!resultado.IsValid) 
            {
                TempData["Errores"] = string.Join("|", resultado.Errors);
                return RedirectToAction("GetEmpleados");
            }

            return View("GetEmpleados", resultado.Value);
        }

        [HttpGet]
        public async Task<IActionResult> Create() //para mostrar el formulario de agregación de empleado
        {
            var departamentos = await _servicioDepartamento.GetDepartamentosAsync();
            ViewBag.Departamentos = new SelectList(departamentos, "IdDepartamento", "Nombre"); //Esto permite el listado desplegable para seleccionar el departamento en el frontend

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostEmpleadoAsync(Empleado empleado) 
        {
            try
            {
                var resultado = await _servicio.PostEmpleadoAsync(empleado);

                if (!resultado.IsValid) 
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return View("Create", empleado);
                }

                return RedirectToAction("GetEmpleados");
            }
            catch (DbUpdateException ex)
            {
                TempData["Errores"] = ex.Message;
                return View("Create", empleado);
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return View("Create", empleado);
            }   
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int id)
        {
            try
            {
                var departamentos = await _servicioDepartamento.GetDepartamentosAsync();
                var resultado = await _servicio.GetEmpleadoByIdAsync(id);
                
                if (!resultado.IsValid)
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return RedirectToAction("GetEmpleados");
                }

                ViewBag.Departamentos = new SelectList(departamentos, "IdDepartamento", "Nombre", resultado?.Value?.IdDepartamento);
                return View(resultado?.Value);
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return RedirectToAction("GetEmpleados");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(Empleado empleado)
        {
            try
            {
                var resultado = await _servicio.PutEmpleadoAsync(empleado);

                if (!resultado.IsValid)
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);

                    var departamentos = await _servicioDepartamento.GetDepartamentosAsync();
                    ViewBag.Departamentos = new SelectList(departamentos, "IdDepartamento", "Nombre", empleado.IdDepartamento);
                    
                    return View(empleado);
                }

                return RedirectToAction("GetEmpleados");
            }
            catch (Exception ex)
            {
                TempData["Errores"] = ex.Message;

                var departamentos = await _servicioDepartamento.GetDepartamentosAsync();
                ViewBag.Departamentos = new SelectList(departamentos, "IdDepartamento", "Nombre", empleado.IdDepartamento);
                
                return View(empleado);
            }
        }

        //[HttpDelete] no se Puede en MVC
        [HttpPost]
        public async Task<IActionResult> DeleteEmpleadoAsync(int id) 
        {
            try
            {
                var resultado = await _servicio.DeleteEmpleadoAsync(id);

                if (!resultado.IsValid)
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                }
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
            }

            return RedirectToAction("GetEmpleados");
        }
    }
}