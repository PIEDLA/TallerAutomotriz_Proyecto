namespace lib_dominio.Entidades
{
    public class Detalles_Pago
    {
        public int Id_detalle { get; set; }
        public int Id_pago { get; set; }
        public string? Metodo_pago { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha_pago { get; set; }
    }
}