using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class Reserva_ServicioAplicacion : IReserva_ServicioAplicacion
    {
        private IConexion? IConexion = null;

        public Reserva_ServicioAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        private string? Validar(Reserva_Servicio entidad)
        {
            bool existe = this.IConexion!.Servicios!.Any(x => x.Id == entidad.Servicio);
            if (!existe)
                return "No existe servicio";
            bool e = this.IConexion!.Reservas!.Any(x => x.Id == entidad.Reserva);
            if (!existe)
                return "No existe reserva";

            return null;
        }

        public Reserva_Servicio? Borrar(Reserva_Servicio? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Reserva del no guardado");

            this.IConexion!.Reserva_Servicio!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Reserva_Servicio? Guardar(Reserva_Servicio? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Reserva del servicio no guardado");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            this.IConexion!.Reserva_Servicio!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Reserva_Servicio> Listar()
        {
            return this.IConexion!.Reserva_Servicio!.Take(5).ToList();
        }

        public Reserva_Servicio? Buscar(int Id)
        {
            var entidad = this.IConexion!.Reserva_Servicio!.Find(Id);
            if (entidad == null)
                throw new Exception("Reserva del servicio no existente");

            return entidad;
        }

        public Reserva_Servicio? Modificar(Reserva_Servicio? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Reserva del servicio no guardado");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            var entry = this.IConexion!.Entry<Reserva_Servicio>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}