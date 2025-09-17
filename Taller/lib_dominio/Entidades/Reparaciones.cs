using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Reparaciones
    {
        public int Id { get; set; }

        public int Id_diagnostico { get; set; }
        [ForeignKey("Id_diagnostico")] public Diagnosticos? _Diagnostico { get; set; }

        public string? Descripcion_trabajo { get; set; }
        public decimal Costo_estimado { get; set; }
        public DateTime Fecha_inicio { get; set; }
    }
}
