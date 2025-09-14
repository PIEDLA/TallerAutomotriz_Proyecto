namespace lib_dominio.Entidades
{
    public class Detalle_Factura
    {
        public int Id_detalle { get; set; }
        public int Id_factura { get; set; }
        public int Id_servicio { get; set; }
        public int Id_prodcuto { get; set; }
        public int Id_repuesto { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
    }
}