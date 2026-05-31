namespace APP_PRUEBA_1.Models.ViewModels
{
    public class EmpleadosPorCursoVM
    {
        public string NombreCurso { get; set; } = null!;
        public IEnumerable<Empleado> Empleados { get; set; } = new List<Empleado>();
    }
}
