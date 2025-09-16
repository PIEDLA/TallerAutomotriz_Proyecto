using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace lib_dominio.Entidades
{
    public class Diagnosticos
    {
        public int Id { get; set; }
        public int Id_vehiculo { get; set; }
        public int Id_empleado { get; set; }
        public string? Descripcion { get; set; }
        public DateTime Fecha { get; set; }
    }
}