using APP_PRUEBA_1.Models.ViewModels;
using APP_PRUEBA_1.Servicios;
using APP_PRUEBA_1.Servicios.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APP_PRUEBA_1.Controllers
{
    public class ReportesController : Controller
    {
        private readonly IReporteService _servicio;
        public ReportesController(IReporteService servicio)
        {
            _servicio = servicio;
        }

        public async Task<IActionResult> EmpleadosPorDepartamento() 
        {
            Result<IEnumerable<EmpleadosPorDepartamentoVM>> resultado;
            try
            {
                resultado = await _servicio.GetEmpleadosPorDepartamentoAsync();
                if (!resultado.IsValid)
                {
                    TempData["Errores"] = string.Join("|", resultado.Errors);
                    return RedirectToAction("GetEmpleados");
                }
                return View(resultado.Value);
            }
            catch (Exception ex) 
            {
                TempData["Errores"] = ex.Message;
                return RedirectToAction("GetEmpleados");
            }
        }
    }
}
