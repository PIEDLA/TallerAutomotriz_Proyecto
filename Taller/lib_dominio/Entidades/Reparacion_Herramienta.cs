using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Reparacion_Herramienta
    {
        public int Id { get; set; }
        public int Id_reparacion { get; set; }
        public int Id_herramienta { get; set; }
    }
}
