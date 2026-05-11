using APP_PRUEBA_1.Models.DTOs;
namespace APP_PRUEBA_1.Models.ViewModels
{
    public class ReporteConFiltrosEmpleadoVM
    {
        public FiltroEmpleadoDTO Filtros { get; set; } = default!;
        public IEnumerable<Empleado> Empleados { get; set; } = Enumerable.Empty<Empleado>();
        public IEnumerable<Departamento> Departamentos { get; set; } = Enumerable.Empty<Departamento>();
    }
}