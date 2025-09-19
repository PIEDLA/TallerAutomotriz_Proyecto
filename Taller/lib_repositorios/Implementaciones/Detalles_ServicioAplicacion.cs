using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class Detalles_ServicioAplicacion : IDetalles_ServicioAplicacion
    {
        private IConexion? IConexion = null;

        public Detalles_ServicioAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        private string? Validar(Detalles_Servicio entidad)
        {
            if (entidad.PrecioServicio < 0) return "No puede haber precio negativo";
            bool existe = this.IConexion!.Servicios!.Any(x => x.Id == entidad.Servicio);
            if (!existe)
                return "No existe servicio";
            bool e = this.IConexion!.Facturas!.Any(x => x.Id == entidad.Factura);
            if (!existe)
                return "No existe factura";

            return null;
        }

        public Detalles_Servicio? Borrar(Detalles_Servicio? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Detalle de servicio no guardado");



            this.IConexion!.Detalles_Servicio!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Detalles_Servicio? Guardar(Detalles_Servicio? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Detalle de servicio no guardado");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            var servicio = this.IConexion!.Servicios!.Find(entidad!.Servicio);
            servicio!.detalles_Servicios!.Add(entidad);

            this.IConexion!.Detalles_Servicio!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Detalles_Servicio> Listar()
        {
            return this.IConexion!.Detalles_Servicio!.Take(5).ToList();
        }

        public Detalles_Servicio? Buscar(int Id)
        {
            var entidad = this.IConexion!.Detalles_Servicio!.Find(Id);
            if (entidad == null)
                throw new Exception("Detalle de servicio no existente");

            return entidad;
        }

        public Detalles_Servicio? Modificar(Detalles_Servicio? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Detalle de servicio no guardado");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            var entry = this.IConexion!.Entry<Detalles_Servicio>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}