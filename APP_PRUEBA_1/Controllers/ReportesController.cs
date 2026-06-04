using APP_PRUEBA_1.Models;
using APP_PRUEBA_1.Models.DTOs;
using APP_PRUEBA_1.Models.ViewModels;
using APP_PRUEBA_1.Servicios;
using APP_PRUEBA_1.Servicios.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Decidir si operador tiene acceso a los reportes.

namespace APP_PRUEBA_1.Controllers
{
    public class ReportesController : Controller
    {
        private readonly IReporteService _servicio;
        public ReportesController(IReporteService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Operador")]
        public IActionResult Reportes() 
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Operador")]
        public async Task<IActionResult> EmpleadosPorDepartamento() 
        {
            Result<IEnumerable<EmpleadosPorDepartamentoVM>> resultado;
            try
            {
                resultado = await _servicio.GetEmpleadosPorDepartamentoAsync();
                if (!resultado.IsValid)
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return RedirectToAction("Reportes");
                }
                return View(resultado.Value);
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return RedirectToAction("Reportes");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Operador")]
        public async Task<IActionResult> EmpleadosAgrupadosPorDepartamento() 
        {
            Result<IEnumerable<EmpleadosAgrupadosPorDepartamentoVM>> resultado;
            try
            {
                resultado = await _servicio.GetEmpleadosAgrupadosPorDepartamentoAsync();
                if (!resultado.IsValid) 
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return RedirectToAction("Reportes");
                }
                return View(resultado.Value);
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return RedirectToAction("Reportes");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ReporteFiltroEspecializado(FiltroEmpleadoDTO filtros) 
        {
            Result<IEnumerable<Empleado>> resultado;
            Result<IEnumerable<Departamento>> departamentos; //nunca lanza Failure, si puede detonar excepción
            try
            {
                resultado = await _servicio.GetEmpleadosReporteFiltros(filtros);
                departamentos = await _servicio.GetDepartamentosAsync();
                if (!resultado.IsValid) 
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return RedirectToAction("ReporteFiltroEspecializado");
                }

                var retornoEmpleados = new ReporteConFiltrosEmpleadoVM
                {
                    Filtros = filtros,
                    Empleados = resultado.Value,
                    Departamentos = departamentos.Value
                };

                return View(retornoEmpleados);
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return RedirectToAction("Reportes");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Operador")]
        public async Task<IActionResult> ReporteEmpleadosPorCurso(int? idCurso) 
        {
            Result<EmpleadosPorCursoVM> resultado;
            try
            {
                ViewBag.Cursos = (await _servicio.GetCursosAsync()).Value;

                if (!idCurso.HasValue)
                    return View();

                resultado = await _servicio.GetEmpleadosPorCursoAsync(idCurso.Value);
                if (!resultado.IsValid) 
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return RedirectToAction("Reportes"); 
                }

                return View(resultado.Value);
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return RedirectToAction("Reportes");
            }
        }
    }
}