namespace APP_PRUEBA_1.Models.DTOs
{
    public record FiltroEmpleadoDTO
    (
        int? IdDepartamento,
        string? Busqueda,
        bool? Estado,
        int? CantidadHijosMin,
        int? CantidadHijosMax,
        DateOnly? FechaIngresoDesde,
        DateOnly? FechaIngresoHasta
    );
}