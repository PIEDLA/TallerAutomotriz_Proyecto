using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Reservas
    {
        public int Id { get; set; }
        public int Id_cliente { get; set; }
        public int Id_sede { get; set; }
        public DateTime Fecha_reserva { get; set; }
        public string? Estado { get; set; }

        List<Reserva_Servicio> reservas_Servicios = new List<Reserva_Servicio>();


        [ForeignKey("Id_cliente")] public Clientes? _Cliente { get; set; }
        [ForeignKey("Id_sede")] public Sedes? _Sede { get; set; }
    }
}