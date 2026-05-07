using APP_PRUEBA_1.Repositorios;
using APP_PRUEBA_1.Servicios.Validation;
using APP_PRUEBA_1.Models.ViewModels;

namespace APP_PRUEBA_1.Servicios
{
    public class ReporteService : IReporteService
    {
        private readonly IEmpleadoRepository _repo;
        public ReporteService(IEmpleadoRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<IEnumerable<EmpleadosPorDepartamentoVM>>> GetEmpleadosPorDepartamentoAsync() 
        {
            var empleadosDesignar = await _repo.GetEmpleadosAsync();
            var empleadosRetornar = empleadosDesignar.GroupBy(e => e.IdDepartamentoNavigation.Nombre).Select(r => new EmpleadosPorDepartamentoVM 
            {
                Departamento = r.Key,
                CantidadEmpleados = r.Count()

            });
            return Result<IEnumerable<EmpleadosPorDepartamentoVM>>.Success(empleadosRetornar);
        }
    }
}