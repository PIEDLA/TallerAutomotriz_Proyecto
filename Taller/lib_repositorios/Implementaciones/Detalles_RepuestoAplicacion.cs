using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class Detalles_RepuestoAplicacion : IDetalles_RepuestoAplicacion
    {
        private IConexion? IConexion = null;

        public Detalles_RepuestoAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        private string? Validar(Detalles_Repuesto entidad)
        {
            bool factura = this.IConexion!.Facturas!.Any(x => x.Id == entidad!.Factura);
            if (!factura)
                return "No existe factura";
            var repuesto = this.IConexion!.Repuestos!.Find(entidad!.Repuesto);
            if (repuesto!.Stock == 0)
                return "Producto fuera de stock";

            return null;
        }

        public Detalles_Repuesto? Borrar(Detalles_Repuesto? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Detalle de repuesto no guardado");



            this.IConexion!.Detalles_Repuesto!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Detalles_Repuesto? Guardar(Detalles_Repuesto? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Detalle de repuesto no guardado");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            var repuesto = this.IConexion!.Repuestos!.Find(entidad!.Repuesto);
            repuesto!.Detalles_Repuesto!.Add(entidad);

            var facturas = this.IConexion!.Facturas!.Find(entidad!.Factura);
            facturas!.Detalles_Repuesto!.Add(entidad);

            this.IConexion!.Detalles_Repuesto!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Detalles_Repuesto> Listar()
        {
            return this.IConexion!.Detalles_Repuesto!.Take(50).ToList();
        }

        public List<Detalles_Repuesto> PorRepuesto(Detalles_Repuesto? entidad)
        {
            return this.IConexion!.Detalles_Repuesto!
                .Where(x => x.Repuesto! == entidad!.Repuesto!)
                .Take(50)
                .ToList();
        }

        public List<Detalles_Repuesto> PorFactura(Detalles_Repuesto? entidad)
        {
            return this.IConexion!.Detalles_Repuesto!
                .Where(x => x.Factura! == entidad!.Factura!)
                .Take(50)
                .ToList();
        }

        public Detalles_Repuesto? Modificar(Detalles_Repuesto? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Detalle de repuesto no guardado");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            var entry = this.IConexion!.Entry<Detalles_Repuesto>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}