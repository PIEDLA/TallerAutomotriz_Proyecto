using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Reparaciones
    {
        public int Id { get; set; }

        public int Id_diagnostico { get; set; }
        [ForeignKey("Id_diagnostico")] public Diagnosticos? _Diagnostico { get; set; }

        public string? Descripcion_trabajo { get; set; }
        public decimal Costo_estimado { get; set; }
        public DateTime Fecha_inicio { get; set; }


        public List<Facturas>? Facturas { get; set; }
        public List<Reparacion_Herramienta>? Reparacion_Herramienta;


    }
}
