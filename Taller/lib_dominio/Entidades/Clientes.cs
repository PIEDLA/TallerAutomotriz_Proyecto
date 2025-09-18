namespace lib_dominio.Entidades
{
    public class Clientes
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }

        List<Vehiculos> vehiculos = new List<Vehiculos>();

        List<Reservas> reservas = new List<Reservas>();

        List<Facturas> facturas = new List<Facturas>();

    }
}