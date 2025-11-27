namespace lib_dominio.Entidades
{
    public class Clientes
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int Documento { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public List<Vehiculos>? Vehiculos { get; set; }
        public List<Reservas>? Reservas { get; set; }
        public List<Facturas>? Facturas { get; set; }
    }
}