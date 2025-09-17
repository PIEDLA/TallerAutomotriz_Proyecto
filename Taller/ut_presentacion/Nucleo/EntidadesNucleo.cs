using lib_dominio.Entidades;
namespace ut_presentacion.Nucleo
{
    public class EntidadesNucleo
    {
        public static Detalles_Producto? Detalles_Producto()
        {
            var entidad = new Detalles_Producto();
            entidad.Cantidad = 1;
            entidad.Factura = 1;
            entidad.Producto = 1;
            return entidad;
        }
        public static Detalles_Repuesto? Detalles_Repuesto()
        {
            var entidad = new Detalles_Repuesto();
            entidad.Cantidad = 1;
            entidad.Factura = 1;
            entidad.Repuesto = 1;
            return entidad;
        }
        public static Detalles_Servicio? Detalles_Servicio()
        {
            var entidad = new Detalles_Servicio();
            entidad.Servicio = 1;
            entidad.Factura = 1;
            return entidad;
        }
        public static Reserva_Servicio? Reserva_Servicio()
        {
            var entidad = new Reserva_Servicio();
            entidad.Reserva= 1;
            entidad.Servicio = 1;
            return entidad;
        }
        public static Diagnosticos? Diagnosticos()
        {
            var entidad = new Diagnosticos();
            entidad.Id_vehiculo = 1;
            entidad.Id_empleado = 1;
            entidad.Descripcion = "Revisión inicial: falla en el motor";
            entidad.Fecha = DateTime.Now;
            return entidad;
        }

        public static Reparaciones? Reparaciones()
        {
            var entidad = new Reparaciones();
            entidad.Id_diagnostico = 1;
            entidad.Descripcion_trabajo = "Cambio de correa de distribución";
            entidad.Costo_estimado = 450000m;
            entidad.Fecha_inicio = DateTime.Now;
            return entidad;
        }

        public static Pagos? Pagos()
        {
            var entidad = new Pagos();
            entidad.Id_factura = 1;
            entidad.Monto_total = 450000m;
            entidad.Fecha_pago = DateTime.Now;
            entidad.Estado = "Completado";
            return entidad;
        }

        public static Repuestos? Repuestos()
        {
            var entidad = new Repuestos();
            entidad.Id_proveedor = 1;
            entidad.Nombre_repuesto = "Filtro de aceite";
            entidad.Marca = "ACPM";
            entidad.Precio = 35000m;
            entidad.Stock = 15;
            return entidad;
        }

        public static Herramientas? Herramientas()
        {
            var entidad = new Herramientas();
            entidad.Nombre = "Llave inglesa";
            entidad.Tipo = "Manual";
            entidad.Estado = "Disponible";
            entidad.Ubicacion = "Estante A-3";
            return entidad;
        }

        public static Reparacion_Herramienta? Reparacion_Herramienta()
        {
            var entidad = new Reparacion_Herramienta();
            entidad.Id_reparacion = 1;
            entidad.Id_herramienta = 1;
            return entidad;
        }

        public static Detalles_Pago? Detalles_Pago()
        {
            var entidad = new Detalles_Pago();
            entidad.Metodo_pago = "Pruebas-";
            entidad.Id_pago = 1;
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
            entidad.Estado = "Pruebas-";
            entidad.Fecha_reserva = DateTime.Now;
            entidad.Id_cliente = 1;
            entidad.Id_sede = 1;
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
            entidad.Id_sede = 1;
            entidad.Cargo = "Pruebas";
            entidad.Telefono = "Pruebas";
            return entidad;
        }

        public static Facturas? Facturas()
        {
            var entidad = new Facturas();
            entidad.Fecha_emision = DateTime.Now;
            entidad.Id_cliente = 1;
            entidad.Id_reparacion = 1;
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
            entidad.Duracion_aprox = "Pruebas";
            return entidad;
        }

        public static Vehiculos? Vehiculos()
        {
            var entidad = new Vehiculos();
            entidad.Placa = "Pruebas-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            entidad.Marca = "Pruebas";
            entidad.Modelo = "Pruebas";
            entidad.Id_cliente = 1;
            return entidad;
        }
    }
}