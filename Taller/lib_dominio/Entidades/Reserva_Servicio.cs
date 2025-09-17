using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Reserva_Servicio
    {
        public int Id { get; set; }
        public int Servicio { get; set; }
        public int Reserva { get; set; }

        [ForeignKey("Servicio")] public Servicios? _Servicio { get; set; }
        [ForeignKey("Reserva")] public Reservas? _Reserva { get; set; }
    }
}
