using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace lib_dominio.Entidades
{
    public class Pagos
    {
        public int Id { get; set; }
        public int Id_factura { get; set; }
        public decimal Monto_total { get; set; }
        public DateTime Fecha_pago { get; set; }
        public string? Estado { get; set; }

        [ForeignKey("Id_factura")] public Facturas? _Factura { get; set; }
    }
}