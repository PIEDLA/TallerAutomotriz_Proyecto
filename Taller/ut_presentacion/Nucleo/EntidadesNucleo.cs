using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{
    public class EntidadesNucleo
    {
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
    }
}     