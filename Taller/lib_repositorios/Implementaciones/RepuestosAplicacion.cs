using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class RepuestosAplicacion : IRepuestosAplicacion
    {
        private IConexion? IConexion = null;

        public RepuestosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public List<Repuestos> Listar()
        {
            return this.IConexion!.Repuestos!.Take(20).ToList();
        }

        public Repuestos? Guardar(Repuestos? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id != 0)
                throw new Exception("Ya se guardó");

            // Operaciones
            entidad._Proveedor = null;

            this.IConexion!.Repuestos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Repuestos? Modificar(Repuestos? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id == 0)
                throw new Exception("No se guardó");

            // Operaciones
            entidad._Proveedor = null;

            var entry = this.IConexion!.Entry<Repuestos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Repuestos? Borrar(Repuestos? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id == 0)
                throw new Exception("No se guardó");

            // Operaciones
            entidad._Proveedor = null;

            this.IConexion!.Repuestos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
