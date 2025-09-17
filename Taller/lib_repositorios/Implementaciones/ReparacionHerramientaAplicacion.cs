using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class ReparacionHerramientaAplicacion : IReparacionHerramientaAplicacion
    {
        private IConexion? IConexion = null;

        public ReparacionHerramientaAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public List<Reparacion_Herramienta> Listar()
        {
            return this.IConexion!.Reparacion_Herramienta!.Take(20).ToList();
        }

        public Reparacion_Herramienta? Guardar(Reparacion_Herramienta? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id != 0)
                throw new Exception("Ya se guardó");

            // Operaciones
            entidad._Reparacion = null;
            entidad._Herramienta = null;

            this.IConexion!.Reparacion_Herramienta!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Reparacion_Herramienta? Modificar(Reparacion_Herramienta? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id == 0)
                throw new Exception("No se guardó");

            // Operaciones
            entidad._Reparacion = null;
            entidad._Herramienta = null;

            var entry = this.IConexion!.Entry<Reparacion_Herramienta>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Reparacion_Herramienta? Borrar(Reparacion_Herramienta? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id == 0)
                throw new Exception("No se guardó");

            // Operaciones
            entidad._Reparacion = null;
            entidad._Herramienta = null;

            this.IConexion!.Reparacion_Herramienta!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
