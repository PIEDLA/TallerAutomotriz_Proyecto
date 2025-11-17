using Azure.Core;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class Detalles_ProductoAplicacion : IDetalles_ProductoAplicacion
    {
        private IConexion? IConexion = null;

        public Detalles_ProductoAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        private string? Validar(Detalles_Producto entidad)
        {
            if (entidad.PrecioProducto < 0) return "No puede haber precio negativo";
            bool existe = this.IConexion!.Productos!.Any(x => x.Id == entidad!.Producto);
            if (!existe)
                return "No existe producto";
            bool e = this.IConexion!.Facturas!.Any(x => x.Id == entidad!.Factura);
            if (!existe)
                return "No existe factura";
            var producto = this.IConexion!.Productos!.Find(entidad!.Producto);
            if (producto!.Stock == 0)
                return "Producto fuera de stock";

            return null;
        }

        public Detalles_Producto? Borrar(Detalles_Producto? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Detalle de producto no guardado");

            this.IConexion!.Detalles_Producto!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Detalles_Producto? Guardar(Detalles_Producto? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Detalle de producto no guardado");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            var producto = this.IConexion!.Productos!.Find(entidad!.Producto);
            producto!.detalles_Productos!.Add(entidad);

            var facturas = this.IConexion!.Facturas!.Find(entidad!.Factura);
            facturas!.Detalles_Producto!.Add(entidad);

            this.IConexion!.Detalles_Producto!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Detalles_Producto> Listar()
        {
            return this.IConexion!.Detalles_Producto!.Take(50).ToList();
        }

        public List<Detalles_Producto> PorProducto(Detalles_Producto? entidad)
        {
            return this.IConexion!.Detalles_Producto!
                .Where(x => x.Producto! == entidad!.Producto!)
                .Take(50)
                .ToList();
        }

        public List<Detalles_Producto> PorFactura(Detalles_Producto? entidad)
        {
            return this.IConexion!.Detalles_Producto!
                .Where(x => x.Factura! == entidad!.Factura!)
                .Take(50)
                .ToList();
        }

        public Detalles_Producto? Modificar(Detalles_Producto? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Detalle de producto no guardado");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            var entry = this.IConexion!.Entry<Detalles_Producto>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}