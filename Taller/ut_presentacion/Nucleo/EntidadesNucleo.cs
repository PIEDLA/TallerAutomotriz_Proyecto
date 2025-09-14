using lib_dominio.Entidades;
namespace ut_presentacion.Nucleo
{
    public class EntidadesNucleo
    {
        public static Detalle_Factura? Detalle_Factura()
        {
            var entidad = new Detalle_Factura();
            entidad.Cantidad = 1;
            entidad.Subtotal = 1.0m;
            return entidad;
        }

        public static Detalles_Pago? Detalles_Pago()
        {
            var entidad = new Detalles_Pago();
            entidad.Metodo_pago = "Pruebas-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            entidad.Monto = 1.0m;
            entidad.Fecha_pago = DateTime.Now;
            return entidad;
        }

        public static Productos? Productos()
        {
            var entidad = new Productos();
            entidad.Nombre_producto = "Pruebas-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            entidad.Precio = 1.0m;
            entidad.Categoria = "Pruebas";
            entidad.Stock = 1;
            return entidad;
        }

        public static Reservas? Reservas()
        {
            var entidad = new Reservas();
            entidad.Estado = "Pruebas-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            entidad.Fecha_reserva = DateTime.Now;
            return entidad;
        }

        public static Sedes? Sedes()
        {
            var entidad = new Sedes();
            entidad.Nombre_sede = "Pruebas-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            entidad.Direccion = "Pruebas";
            entidad.Telefono = "Pruebas";
            entidad.Ciudad = "Pruebas";
            return entidad;
        }
    }
}