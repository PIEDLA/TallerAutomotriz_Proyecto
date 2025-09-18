using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Facturas
    {
        public int Id { get; set; }
        public int Id_cliente { get; set; }
        public int Id_reparacion { get; set; }
        public DateTime Fecha_emision { get; set; }
        public decimal Total { get; set; }

        List<Detalles_Producto> detalles_Productos = new List<Detalles_Producto>();
        List<Detalles_Repuesto> detalles_Repuestos = new List<Detalles_Repuesto>();
        List<Detalles_Servicio> detalles_Servicios = new List<Detalles_Servicio>();
        List<Pagos> pagos = new List<Pagos>();

        public List<Detalles_Producto>? Detalles_Productos;
        public List<Detalles_Repuesto>? Detalles_Repuestos;
        public List<Detalles_Servicio>? Detalles_Servicios;
        public List<Pagos>? Pagos;

        [ForeignKey("Id_cliente")] public Clientes? _Cliente { get; set; }
        [ForeignKey("Id_reparacion")] public Reparaciones? _Reparacion { get; set; }
    }
}