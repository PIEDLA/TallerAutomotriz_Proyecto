using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace lib_repositorios.Implementaciones
{
    public class ReservasAplicacion : IReservasAplicacion
    {
        private IConexion? IConexion = null;

        public ReservasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        private string? Validar(Reservas entidad)
        {
            bool existe = this.IConexion!.Clientes!.Any(x => x.Id == entidad.Id_cliente);
            if (!existe)
                return "No existe cliente";
            bool e = this.IConexion!.Sedes!.Any(x => x.Id == entidad.Id_sede);
            if (!existe)
                return "No existe sede";

            return null;
        }

        public Reservas? Borrar(Reservas? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Reserva no guardado");

            if(!String.Equals(entidad!.Estado, "Cancelada"))
                throw new Exception("La reserva aún se encuentra en proceso");

            this.IConexion!.Reservas!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Reservas? Guardar(Reservas? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Reserva no guardada");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            var sede = this.IConexion!.Sedes!.Find(entidad!.Id_sede);
            sede!.reservas!.Add(entidad);

            var cliente = this.IConexion!.Clientes!.Find(entidad!.Id_cliente);
            cliente!.Reservas!.Add(entidad);

            this.IConexion!.Reservas!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Reservas> Listar()
        {
            return this.IConexion!.Reservas!
                .Take(50)
                .Include(c => c._Cliente)
                .Include(s => s._Cliente)
                .ToList();
        }

        public List<Reservas> PorEstado(Reservas? entidad)
        {
            return this.IConexion!.Reservas!
                .Where(x => x.Estado!.Contains(entidad!.Estado!))
                .Take(50)
                .ToList();
        }

        public Reservas? Modificar(Reservas? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Reserva no guardado");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            var entry = this.IConexion!.Entry<Reservas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}