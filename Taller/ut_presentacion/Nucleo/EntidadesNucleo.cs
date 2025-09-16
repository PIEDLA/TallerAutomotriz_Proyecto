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
        public static Clientes? Clientes()
        {
            var entidad = new Clientes();
            entidad.Nombre = "Pruebas-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            entidad.Apellido = "Pruebas";
            entidad.Telefono = "Pruebas";
            entidad.Correo = "Pruebas";
            return entidad;
        }

        public static Empleados? Empleados()
        {
            var entidad = new Empleados();
            entidad.Nombre = "Pruebas-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            entidad.Apellido = "Pruebas";
            entidad.Cargo = "Pruebas";
            entidad.Telefono = "Pruebas";
            return entidad;
        }

        public static Facturas? Facturas()
        {
            var entidad = new Facturas();
            entidad.Fecha_emision = DateTime.Now;
            entidad.Total = 1.0m;
            return entidad;
        }

        public static Proveedores? Proveedores()
        {
            var entidad = new Proveedores();
            entidad.Nombre = "Pruebas-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            entidad.Telefono = "Pruebas";
            entidad.Correo = "Pruebas";
            entidad.Direccion = "Pruebas";
            return entidad;
        }

        public static Servicios? Servicios()
        {
            var entidad = new Servicios();
            entidad.Nombre_servicio = "Pruebas-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            entidad.Descripcion = "Pruebas";
            entidad.Precio = 1.0m;
            entidad.Duracion_aprox = 1;
            return entidad;
        }

        public static Vehiculos? Vehiculos()
        {
            var entidad = new Vehiculos();
            entidad.Placa = "Pruebas-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            entidad.Marca = "Pruebas";
            entidad.Modelo = "Pruebas";
            return entidad;
        }
    }
}