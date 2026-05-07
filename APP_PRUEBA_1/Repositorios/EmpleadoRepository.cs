using APP_PRUEBA_1.Models;
using Microsoft.EntityFrameworkCore;

namespace APP_PRUEBA_1.Repositorios
{
    //El repositorio solo debe ejecutar las acciones solicitadas, más no validar y manejar excepciones.
    //Esto significa que asume que las acciones que se le solicitan son válidas de realizar cuando se llaman desde el servicio.
    //Solo pueden ocurrir excepciones de la base de datos que se manejen en el controlador.
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly RRHH2026_Db_Context _contexto;
        public EmpleadoRepository(RRHH2026_Db_Context contexto)
        {
            _contexto = contexto;
        }

        public async Task<ICollection<Empleado>> GetEmpleadosAsync() 
        {
            return await _contexto.Empleados.Include(e => e.IdDepartamentoNavigation).ToListAsync(); //Include para mostrar el nombre del departamento
        }

        public async Task<Empleado?> GetEmpleadoByIdAsync(int id) 
        {
            var empleado = await _contexto.Empleados.Include(e => e.IdDepartamentoNavigation).FirstOrDefaultAsync(e => e.IdEmpleado.Equals(id));
            return empleado;
        }

        //Repo para Filtro de Búsqueda ↨
        public async Task<ICollection<Empleado>> GetEmpleadosByNameOrLastNameAsync(string busqueda) 
        {
            var empleados = await _contexto.Empleados.Where(e => e.Nombre.Contains(busqueda) || e.Apellido.Contains(busqueda)).ToListAsync();
            return empleados;
        }
        //Repo para Filtro de Búsqueda ↨
        public async Task PostEmpleadoAsync(Empleado empleado) 
        {
            await _contexto.Empleados.AddAsync(empleado);
            await _contexto.SaveChangesAsync();
        }

        public async Task PutEmpleadoAsync(Empleado empleado)
        {
            var existe = await _contexto.Empleados.FindAsync(empleado.IdEmpleado);

            existe.Nombre = empleado.Nombre;
            existe.Apellido = empleado.Apellido;
            existe.Dni = empleado.Dni;
            existe.Estado = empleado.Estado;
            existe.FechaIngreso = empleado.FechaIngreso;
            existe.CantidadHijos = empleado.CantidadHijos;
            existe.IdDepartamento = empleado.IdDepartamento;

            await _contexto.SaveChangesAsync();
        }

        public async Task DeleteEmpleadoByIdAsync(int id)
        {
            var existe = await _contexto.Empleados.FindAsync(id);

            _contexto.Empleados.Remove(existe);
            await _contexto.SaveChangesAsync();
        }
        //En desuso
        public async Task DeleteEmpleadoAsync(Empleado empleado) 
        {
            _contexto.Empleados.Attach(empleado);
            _contexto.Empleados.Remove(empleado);

            await _contexto.SaveChangesAsync();
        }
    }
}
/*Antes Retornaban bool: 
-DeleteEmpleadoByIdAsync()
-PutEmpleadoAsync()
*/

/* Previo al cambio de enfoque:
public async Task PutEmpleadoAsync(Empleados empleado)
        {
            var entidad = new Empleados
            {
                IdEmpleado = empleado.IdEmpleado
            };

            _contexto.Empleados.Attach(entidad); //Adjuntar al contexto como entidad a modificar para evitar doble búsqueda

            //Designar propiedades a modificar
            entidad.Nombre = empleado.Nombre;
            entidad.Apellido = empleado.Apellido;
            entidad.FechaIngreso = empleado.FechaIngreso;
            entidad.IdDepartamento = empleado.IdDepartamento;

            //Marcarlas como modificadas para que al hacer savechanges se genere una consulta que las actualize
            _contexto.Entry(entidad).Property(e => e.Nombre).IsModified = true;
            _contexto.Entry(entidad).Property(e => e.Apellido).IsModified = true;
            _contexto.Entry(entidad).Property(e => e.FechaIngreso).IsModified = true;
            _contexto.Entry(entidad).Property(e => e.IdDepartamento).IsModified = true;

            await _contexto.SaveChangesAsync();
        }

        public async Task DeleteEmpleadoByIdAsync(int id) 
        {
            var entidad = new Empleados
            {
                IdEmpleado = id
            };

            _contexto.Empleados.Attach(entidad);
            _contexto.Empleados.Remove(entidad);

            await _contexto.SaveChangesAsync();
        }

*/

/*
 //Repo simplificado:
public async Task UpdateEmpleadoAsync(Empleados empleado)
{
    _contexto.Empleados.Update(empleado);
    await _contexto.SaveChangesAsync();
}
*/