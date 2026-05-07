#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APP_PRUEBA_1.Models;

public partial class Departamento
{
    [Key]
    public int IdDepartamento { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; }

    [Required]
    [StringLength(80)]
    [Unicode(false)]
    public string Ubicacion { get; set; }

    [InverseProperty("IdDepartamentoNavigation")]
    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}