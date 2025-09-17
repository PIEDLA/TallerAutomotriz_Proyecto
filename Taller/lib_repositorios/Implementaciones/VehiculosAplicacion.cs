using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class VehiculosAplicacion : IVehiculosAplicacion
    {
        private IConexion? IConexion = null;

        public VehiculosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Vehiculos? Borrar(Vehiculos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Operaciones
            entidad._Cliente = null;

            this.IConexion!.Vehiculos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Vehiculos? Guardar(Vehiculos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Operaciones
            entidad._Cliente = null;

            this.IConexion!.Vehiculos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Vehiculos> Listar()
        {
            return this.IConexion!.Vehiculos!.Take(20).ToList();
        }

        public Vehiculos? Modificar(Vehiculos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Operaciones
            entidad._Cliente = null;

            var entry = this.IConexion!.Entry<Vehiculos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}