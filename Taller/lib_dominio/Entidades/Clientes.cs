namespace lib_dominio.Entidades
{
    public class Clientes
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }

        public List<Vehiculos>? vehiculos;

        public List<Reservas>? reservas;

        public List<Facturas>? facturas;

    }
}