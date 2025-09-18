namespace lib_dominio.Entidades
{
    public class Servicios
    {
        public int Id { get; set; }
        public string? Nombre_servicio { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public String? Duracion_aprox { get; set; }

<<<<<<< HEAD
        public List<Reserva_Servicio>? Reserva_Servicios;
        public List<Detalles_Servicio>? Detalles_Servicios;
=======

        public List<Detalles_Servicio>? Detalles_Servicio;

        public List<Reserva_Servicio>? Reserva_Servicio;

>>>>>>> main
    }
}