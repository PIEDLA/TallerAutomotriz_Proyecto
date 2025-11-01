using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Funciones
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Permisos { get; set; }

        public List<Usuarios>? Usuarios;
    }
}