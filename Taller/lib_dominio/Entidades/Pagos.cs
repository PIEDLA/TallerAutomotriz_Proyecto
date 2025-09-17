using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Pagos
    {
        public int Id { get; set; }

        public int Id_factura { get; set; }
        [ForeignKey("Id_factura")] public Facturas? _Factura { get; set; }

        public decimal Monto_total { get; set; }
        public DateTime Fecha_pago { get; set; }
        public string? Estado { get; set; }
    }
}
