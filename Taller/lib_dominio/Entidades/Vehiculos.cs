using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Vehiculos
    {
        public int Id { get; set; }
        public int Id_cliente { get; set; }
        public string? Placa { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }

        List<Diagnosticos> diagnosticos = new List<Diagnosticos>();


        [ForeignKey("Id_cliente")] public Clientes? _Cliente { get; set; }
    }
}