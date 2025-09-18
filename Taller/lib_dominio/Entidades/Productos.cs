namespace lib_dominio.Entidades
{
    public class Productos
    {
        public int Id { get; set; }
        public string? Nombre_producto { get; set; }
        public decimal Precio { get; set; }
        public string? Categoria { get; set; }
        public int Stock { get; set; }

        List<Detalles_Producto> detalles_Productos = new List<Detalles_Producto>();

    }
}