using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Detalles_Pago
    {
        public int Id { get; set; }
        public int Id_pago { get; set; }
        public string? Metodo_pago { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha_pago { get; set; }

        [ForeignKey("Id_pago")] public Pagos? _Pago { get; set; }
    }
}