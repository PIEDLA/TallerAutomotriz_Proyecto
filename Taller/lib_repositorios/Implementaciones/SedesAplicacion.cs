using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class SedesAplicacion : ISedesAplicacion
    {
        private IConexion? IConexion = null;

        public SedesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        private string? Validar(Sedes entidad)
        {
            if (string.IsNullOrWhiteSpace(entidad.Nombre_sede)) return "Nombre de sede requerido";
            if (string.IsNullOrWhiteSpace(entidad.Telefono)) return "Telefono de sede requerido";
            if (string.IsNullOrWhiteSpace(entidad.Direccion)) return "Direccion de sede requerida";
            if (string.IsNullOrWhiteSpace(entidad.Ciudad)) return "Ciudad de sede requerida";

            return null;
        }

        public Sedes? Borrar(Sedes? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Sede no guardada");



            this.IConexion!.Sedes!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Sedes? Guardar(Sedes? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Sede no guardada");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            this.IConexion!.Sedes!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Sedes> Listar()
        {
            return this.IConexion!.Sedes!.Take(5).ToList();
        }

        public Sedes? Buscar(int Id)
        {
            var entidad = this.IConexion!.Sedes!.Find(Id);
            if (entidad == null)
                throw new Exception("Sede no existente");

            return entidad;
        }

        public Sedes? Modificar(Sedes? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Sede no guardada");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            var entry = this.IConexion!.Entry<Sedes>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}