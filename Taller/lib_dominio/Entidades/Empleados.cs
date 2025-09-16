namespace lib_dominio.Entidades
{
    public class Empleados
    {
        public int Id { get; set; }
        public int Id_sede { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Cargo { get; set; }
        public string? Telefono { get; set; }
    }
}