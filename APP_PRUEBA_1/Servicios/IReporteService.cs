using APP_PRUEBA_1.Models.ViewModels;
using APP_PRUEBA_1.Servicios.Validation;

namespace APP_PRUEBA_1.Servicios
{
    public interface IReporteService
    {
        Task<Result<IEnumerable<EmpleadosPorDepartamentoVM>>> GetEmpleadosPorDepartamentoAsync();
    }
}
