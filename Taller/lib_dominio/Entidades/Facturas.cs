using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Facturas
    {
        public int Id { get; set; }
        public int Id_cliente { get; set; }
        public int Id_reparacion { get; set; }
        public DateTime Fecha_emision { get; set; }
        public decimal Total { get; set; }

        [ForeignKey("Id_cliente")] public Clientes? _Cliente { get; set; }
        [ForeignKey("Id_reparacion")] public Reparaciones? _Reparacion { get; set; }
    }
}