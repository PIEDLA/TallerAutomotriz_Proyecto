namespace lib_dominio.Entidades
{
    public class Herramientas
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Tipo { get; set; }
        public string? Estado { get; set; }
        public string? Ubicacion { get; set; }

        public List<Reparacion_Herramienta>? Reparacion_Herramienta;
    }
}
