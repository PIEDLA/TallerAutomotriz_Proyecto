using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Reparaciones
    {
        public int Id { get; set; }
        public int Id_diagnostico { get; set; }
        public string? Descripcion_trabajo { get; set; }
        public decimal Costo_estimado { get; set; }
        public DateTime Fecha_inicio { get; set; }
    }
}
