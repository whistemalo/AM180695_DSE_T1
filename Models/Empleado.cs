using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace AM180695.Models
{
    public class Empleado
    {
        [Key]
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Cargo { get; set; }
        public decimal SueldoBase { get; set; }
    }
    public class EmpleadoDBContex : DbContext
    {
        public DbSet<Empleado> Empleados { get; set; }
    }
}