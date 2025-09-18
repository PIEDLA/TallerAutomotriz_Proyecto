using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Detalles_Repuesto
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public int Repuesto { get; set; }
        public int Factura { get; set; }

        [ForeignKey("Repuesto")] public Repuestos? _Repuesto { get; set; }
        [ForeignKey("Factura")] public Facturas? _Factura { get; set; }
    }

}
