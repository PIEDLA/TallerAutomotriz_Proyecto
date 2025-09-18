using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lib_dominio.Entidades
{
    public class Reparacion_Herramienta
    {
        public int Id { get; set; }

        public int Id_reparacion { get; set; }
        [ForeignKey("Id_reparacion")] public Reparaciones? _Reparacion { get; set; }

        public int Id_herramienta { get; set; }
        [ForeignKey("Id_herramienta")] public Herramientas? _Herramienta { get; set; }
    }
}