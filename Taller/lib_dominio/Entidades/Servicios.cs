namespace lib_dominio.Entidades
{
    public class Servicios
    {
        public int Id { get; set; }
        public string? Nombre_servicio { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public String? Duracion_aprox { get; set; }

        List<Reserva_Servicio> reservas_Servicios = new List<Reserva_Servicio>();
        List<Detalles_Servicio> detalles_Servicios = new List<Detalles_Servicio>();

    }
}