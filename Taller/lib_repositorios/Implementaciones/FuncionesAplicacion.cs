using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class FuncionesAplicacion : IFuncionesAplicacion
    {
        private IConexion? IConexion = null;

        public FuncionesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Funciones? Borrar(Funciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Funciones!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Funciones? Guardar(Funciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Operaciones

            var vehiculoExistente = this.IConexion!.Funciones!.FirstOrDefault(x => x.Nombre!.ToUpper() == entidad.Nombre!.ToUpper());

            if (vehiculoExistente != null)
                throw new Exception("Ya existe un vehículo registrado con esta placa");

            if (string.IsNullOrWhiteSpace(entidad.Nombre))
                throw new Exception("El nombre es obligatoria.");

            if (string.IsNullOrWhiteSpace(entidad.Permisos))
                throw new Exception("Los permisos son obligatorios.");

            this.IConexion!.Funciones!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Funciones> Listar()
        {
            return this.IConexion!.Funciones!.ToList();
        }

        public Funciones? Modificar(Funciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            if (string.IsNullOrWhiteSpace(entidad.Nombre))
                throw new Exception("El nombre es obligatoria.");

            if (string.IsNullOrWhiteSpace(entidad.Permisos))
                throw new Exception("Los permisos son obligatorios.");

            var entry = this.IConexion!.Entry<Funciones>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}