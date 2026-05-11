using APP_PRUEBA_1.Models;
using APP_PRUEBA_1.Models.DTOs;
using APP_PRUEBA_1.Models.ViewModels;
using APP_PRUEBA_1.Servicios.Validation;

namespace APP_PRUEBA_1.Servicios
{
    public interface IReporteService
    {
        Task<Result<IEnumerable<EmpleadosPorDepartamentoVM>>> GetEmpleadosPorDepartamentoAsync();
        Task<Result<IEnumerable<EmpleadosAgrupadosPorDepartamentoVM>>> GetEmpleadosAgrupadosPorDepartamentoAsync();
        Task<Result<IEnumerable<Empleado>>> GetEmpleadosReporteFiltros(FiltroEmpleadoDTO filtro);
        Task<Result<IEnumerable<Departamento>>> GetDepartamentosAsync();
    }
}
