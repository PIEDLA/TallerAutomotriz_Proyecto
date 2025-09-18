using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Detalles_Producto
    {
        public int Id { get; set; }
        public decimal PrecioProducto { get; set; }
        public int Producto { get; set; }
        public int Factura { get; set; }

        [ForeignKey("Producto")] public Productos? _Producto { get; set; }
        [ForeignKey("Factura")] public Facturas? _Factura { get; set; }
    }
}
