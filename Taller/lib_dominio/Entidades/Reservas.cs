namespace lib_dominio.Entidades
{
    public class Reservas
    {
        public int Id_reserva { get; set; }
        public int Id_cliente { get; set; }
        public int Id_servicio { get; set; }
        public int Id_sede { get; set; }
        public DateTime Fecha_reserva { get; set; }
        public string? Estado { get; set; }
    }
}