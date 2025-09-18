using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class FacturasAplicacion : IFacturasAplicacion
    {
        private IConexion? IConexion = null;

        public FacturasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public List<Facturas> Listar()
        {
            return this.IConexion!.Facturas!
                .Include(f => f._Cliente)
                .Include(f => f._Reparacion)
                .ToList();
        }

        public Facturas? Guardar(Facturas? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información de la factura");

            if (entidad.Id != 0)
                throw new Exception("La factura ya existe");

            if (entidad.Id_cliente <= 0)
                throw new Exception("Debe especificar un cliente");

            if (entidad.Id_reparacion <= 0)
                throw new Exception("Debe especificar una reparación");

            if (entidad.Total <= 0)
                throw new Exception("El total debe ser mayor a 0");

            if (entidad.Fecha_emision < DateTime.Now)
                throw new Exception("La fecha no puede ser pasada");

            bool tieneDetalles = (entidad.Detalles_Productos != null && entidad.Detalles_Productos.Count > 0) ||
                                 (entidad.Detalles_Servicios != null && entidad.Detalles_Servicios.Count > 0) ||
                                 (entidad.Detalles_Repuestos != null && entidad.Detalles_Repuestos.Count > 0);

            if (!tieneDetalles)
                throw new Exception("La factura debe tener al menos un detalle (producto, servicio o repuesto)");

            entidad._Cliente = null;
            entidad._Reparacion = null;

            this.IConexion!.Facturas!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Facturas? Modificar(Facturas? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información de la factura");

            if (entidad.Id == 0)
                throw new Exception("La factura no existe");

            if (entidad.Total <= 0)
                throw new Exception("El total debe ser mayor a 0");

            entidad._Cliente = null;
            entidad._Reparacion = null;

            var entry = this.IConexion!.Entry<Facturas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Facturas? Borrar(Facturas? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información de la factura");

            if (entidad.Id == 0)
                throw new Exception("La factura no existe");

            entidad._Cliente = null;
            entidad._Reparacion = null;

            this.IConexion!.Facturas!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Facturas> ListarPorCliente(int idCliente)
        {
            return this.IConexion!.Facturas!
                .Where(f => f.Id_cliente == idCliente)
                .Include(f => f._Cliente)
                .ToList();
        }
    }
}