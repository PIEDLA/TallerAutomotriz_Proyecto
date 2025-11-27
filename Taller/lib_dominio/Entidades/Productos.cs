using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Productos
    {
        public int Id { get; set; }
        public string? Nombre_producto { get; set; }
        public int Id_proveedor { get; set; }
        public decimal Precio { get; set; }
        public string? Categoria { get; set; }
        public int Stock { get; set; }
        [ForeignKey("Id_proveedor")] public Proveedores? _Proveedor { get; set; }

        public List<Detalles_Producto>? detalles_Productos { get; set; }

    }
}