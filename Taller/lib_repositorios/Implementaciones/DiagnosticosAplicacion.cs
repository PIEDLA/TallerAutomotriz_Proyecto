using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class DiagnosticosAplicacion : IDiagnosticosAplicacion
    {
        private IConexion? IConexion = null;

        public DiagnosticosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public List<Diagnosticos> Listar()
        {
            return this.IConexion!.Diagnosticos!.Take(20).ToList();
        }

        public Diagnosticos? Guardar(Diagnosticos? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id != 0)
                throw new Exception("Ya se guardó");

            // Operaciones
            entidad._Vehiculo = null;
            entidad._Empleado = null;

            this.IConexion!.Diagnosticos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Diagnosticos? Modificar(Diagnosticos? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id == 0)
                throw new Exception("No se guardó");

            // Operaciones
            entidad._Vehiculo = null;
            entidad._Empleado = null;

            var entry = this.IConexion!.Entry<Diagnosticos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Diagnosticos? Borrar(Diagnosticos? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id == 0)
                throw new Exception("No se guardó");

            // Operaciones
            entidad._Vehiculo = null;
            entidad._Empleado = null;

            this.IConexion!.Diagnosticos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
