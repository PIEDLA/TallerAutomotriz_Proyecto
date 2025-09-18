using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public Proveedores? Borrar(Proveedores? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Operaciones

            var proveedorExistente = this.IConexion!.Proveedores!.FirstOrDefault(x => x.Id == entidad.Id);

            if (proveedorExistente == null)
                throw new Exception("El proveedor que intenta eliminar no existe");

            this.IConexion!.Proveedores!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Proveedores? Guardar(Proveedores? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Operaciones
            if (string.IsNullOrWhiteSpace(entidad.Telefono) && string.IsNullOrWhiteSpace(entidad.Correo))
                throw new Exception("Debe proporcionar al menos un teléfono o correo electrónico");

            this.IConexion!.Proveedores!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Proveedores> Listar()
        {
            return this.IConexion!.Proveedores!.ToList();
        }

        public List<Proveedores> BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("Debe especificar un nombre para buscar");

            return this.IConexion!.Proveedores!.Where(x => x.Nombre!.Contains(nombre))
                                            .OrderBy(x => x.Nombre).ToList();
        }

        public Proveedores? Modificar(Proveedores? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Operaciones
            var proveedorExistente = this.IConexion!.Proveedores!.FirstOrDefault(x => x.Id == entidad.Id);

            if (proveedorExistente == null)
                throw new Exception("El proveedor que intenta modificar no existe");

            var entry = this.IConexion!.Entry<Proveedores>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
