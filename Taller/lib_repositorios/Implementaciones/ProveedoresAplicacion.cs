using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace lib_repositorios.Implementaciones
{
    public class ProveedoresAplicacion : IProveedoresAplicacion
    {
        private IConexion? IConexion = null;

        public ProveedoresAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        private string? Validar(Proveedores entidad)
        {
            if (string.IsNullOrWhiteSpace(entidad.Nombre))
                return("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(entidad.Telefono))
                return("El teléfono es obligatorio.");

            if (string.IsNullOrWhiteSpace(entidad.Correo))
                return("El correo es obligatorio.");

            return null;
        }

        public Proveedores? Borrar(Proveedores? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información del proveedor");

            if (entidad!.Id == 0)
                throw new Exception("El proveedor no existe");

            this.IConexion!.Proveedores!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Proveedores? Guardar(Proveedores? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información del proveedor");

            if (entidad.Id != 0)
                throw new Exception("El proveedor ya existe");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            this.IConexion!.Proveedores!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Proveedores> Listar()
        {
            return this.IConexion!.Proveedores!.ToList();
        }

        public List<Proveedores> PorNombre(Proveedores? entidad)
        {
            return this.IConexion!.Proveedores!
                .Where(x => x.Nombre!.Contains(entidad!.Nombre!))
                .Take(50)
                .ToList();
        }

        public List<Proveedores> PorNIT(Proveedores? entidad)
        {
            if (entidad!.NIT == 0)
                return this.IConexion!.Proveedores!
                    .ToList();

            return this.IConexion!.Proveedores!
                .Where(x => x.NIT == entidad.NIT)
                .ToList();
        }

        public Proveedores? Modificar(Proveedores? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            var entry = this.IConexion!.Entry<Proveedores>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
