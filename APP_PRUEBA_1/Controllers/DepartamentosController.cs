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
    public class DepartamentosController : Controller
    {
        private readonly IDepartamentoService _servicio;
        public DepartamentosController(IDepartamentoService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartamentosAsync() 
        {
            try
            {
                var departamentos = await _servicio.GetDepartamentosAsync();
                return View(departamentos);
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartamentoByIdAsync(int id) 
        {
            Result<Departamento> resultado;
            try
            {
                resultado = await _servicio.GetDepartamentoByIdAsync(id);
                if (!resultado.IsValid)
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return RedirectToAction("GetDepartamentos");
                }
                return View("Detalles", resultado.Value);
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return RedirectToAction("GetDepartamentos");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartamentoByNameOrIdAsync(string busqueda) 
        {
            Result<IEnumerable<Departamento>> resultado;
            try
            {
                resultado = await _servicio.GetDepartamentosByNameOrIdAsync(busqueda);
                if (!resultado.IsValid)
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return RedirectToAction("GetDepartamentos");
                }
                return View("GetDepartamentos", resultado.Value);
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return RedirectToAction("GetDepartamentos");
            }
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostDepartamentoAsync(Departamento departamento) 
        {
            Result<Departamento> resultado;
            try
            {
                resultado = await _servicio.PostDepartamentoAsync(departamento);
                if (!resultado.IsValid)
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return View("Create", departamento);
                }
                return RedirectToAction("GetDepartamentos");
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return View("Create", departamento);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int id) 
        {
            Result<Departamento> resultado;
            try
            {
                resultado = await _servicio.GetDepartamentoByIdAsync(id);
                if (!resultado.IsValid)
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return RedirectToAction("GetDepartamentos");
                }
                return View(resultado.Value);
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return RedirectToAction("GetDepartamentos");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(Departamento departamento) 
        {
            Result<Departamento> resultado;
            try
            {
                resultado = await _servicio.PutDepartamentoAsync(departamento);
                if (!resultado.IsValid)
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return View(departamento);
                }
                return RedirectToAction("GetDepartamentos");
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return View(departamento);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDepartamentoAsync(int id) 
        {
            Result<Departamento> resultado;
            try
            {
                resultado = await _servicio.DeleteDepartamentoByIdAsync(id);
                if (!resultado.IsValid)
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return RedirectToAction("GetDepartamentos");
                }
                return RedirectToAction("GetDepartamentos");
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return RedirectToAction("GetDepartamentos");
            }
        }
    }
}