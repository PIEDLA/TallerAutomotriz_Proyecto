using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class ReparacionesAplicacion : IReparacionesAplicacion
    {
        private IConexion? IConexion = null;

        public ReparacionesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public List<Reparaciones> Listar()
        {
            return this.IConexion!.Reparaciones!.Take(20).ToList();
        }

        public Reparaciones? Guardar(Reparaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id != 0)
                throw new Exception("Ya se guardó");

            // Operaciones
            entidad._Diagnostico = null;

            this.IConexion!.Reparaciones!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Reparaciones? Modificar(Reparaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id == 0)
                throw new Exception("No se guardó");

            // Operaciones
            entidad._Diagnostico = null;

            var entry = this.IConexion!.Entry<Reparaciones>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Reparaciones? Borrar(Reparaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id == 0)
                throw new Exception("No se guardó");

            // Operaciones
            entidad._Diagnostico = null;

            this.IConexion!.Reparaciones!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
