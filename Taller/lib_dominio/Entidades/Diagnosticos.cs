using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Diagnosticos
    {
        public int Id { get; set; }

        public int Id_vehiculo { get; set; }
        [ForeignKey("Id_vehiculo")] public Vehiculos? _Vehiculo { get; set; }

        public int Id_empleado { get; set; }
        [ForeignKey("Id_empleado")] public Empleados? _Empleado { get; set; }

        public string? Descripcion { get; set; }
        public DateTime Fecha { get; set; }
    }
}
