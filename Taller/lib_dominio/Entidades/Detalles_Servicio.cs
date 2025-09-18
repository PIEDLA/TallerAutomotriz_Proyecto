using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Detalles_Servicio
    {
        public int Id { get; set; }
        public decimal PrecioServicio { get; set; }
        public int Servicio { get; set; }
        public int Factura { get; set; }

        [ForeignKey("Servicio")] public Servicios? _Servicio { get; set; }
        [ForeignKey("Factura")] public Facturas? _Factura { get; set; }
    }
}
