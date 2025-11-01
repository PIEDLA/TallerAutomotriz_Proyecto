using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Contraseña { get; set; }
        public int Funcion { get; set; }

        [ForeignKey("Funcion")] public Funciones? _Funcion { get; set; }
    }
}