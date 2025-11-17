using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace lib_repositorios.Implementaciones
{
    public class Detalles_PagoAplicacion : IDetalles_PagoAplicacion
    {
        private IConexion? IConexion = null;

        public Detalles_PagoAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        private string? Validar(Detalles_Pago entidad)
        {
            if (string.IsNullOrWhiteSpace(entidad.Metodo_pago)) return "Método de pago requerido";
            if (entidad.Monto < 0) return "No puede haber monto negativo";
            bool existe = this.IConexion!.Pagos!.Any(x => x.Id == entidad.Id_pago);
            if (!existe)
                throw new Exception("No existe el pago");
            return null;
        }

        public Detalles_Pago? Borrar(Detalles_Pago? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Detalle de pago no guardado");

            this.IConexion!.Detalles_Pago!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Detalles_Pago? Guardar(Detalles_Pago? entidad)
        {

            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Detalle de pago no guardado");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            var pago = this.IConexion!.Pagos!.Find(entidad!.Id_pago);
            pago!.Detalles_Pago!.Add(entidad);


            this.IConexion!.Detalles_Pago!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Detalles_Pago> Listar()
        {
            return this.IConexion!.Detalles_Pago!
                .Take(50)
                .Include(c => c._Pago)
                .ToList();
        }

        public List<Detalles_Pago> PorMetodoPago(Detalles_Pago? entidad)
        {
            return this.IConexion!.Detalles_Pago!
                .Where(x => x.Metodo_pago!.Contains(entidad!.Metodo_pago!))
                .Take(50)
                .ToList();
        }

        public Detalles_Pago? Modificar(Detalles_Pago? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Detalle de pago no guardado");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            var entry = this.IConexion!.Entry<Detalles_Pago>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}