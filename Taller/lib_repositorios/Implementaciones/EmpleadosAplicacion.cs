using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class EmpleadosAplicacion : IEmpleadosAplicacion
    {
        private IConexion? IConexion = null;

        public EmpleadosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Empleados? Borrar(Empleados? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Operaciones
            entidad._Sede = null;

            var empleadoExistente = this.IConexion!.Empleados!.Include(x => x._Sede).FirstOrDefault(x => x.Id == entidad.Id);

            if (empleadoExistente == null)
                throw new Exception("El empleado que intenta eliminar no existe");

            this.IConexion!.Empleados!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Empleados? Guardar(Empleados? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Operaciones
            entidad._Sede = null;

            this.IConexion!.Empleados!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Empleados> Listar()
        {
            return this.IConexion!.Empleados!.ToList();
        }

        public List<Empleados> ListarPorSede(int sedeId)
        {
            if (sedeId <= 0)
                throw new Exception("Debe especificar una sede válida");

            return this.IConexion!.Empleados!
                .Include(x => x._Sede).Where(x => x.Id_sede == sedeId).ToList();
        }

        public Empleados? Modificar(Empleados? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Operaciones
            entidad._Sede = null;

            var empleadoExistente = this.IConexion!.Empleados!.FirstOrDefault(x => x.Id == entidad.Id);

            if (empleadoExistente == null)
                throw new Exception("El empleado que intenta modificar no existe");

            var entry = this.IConexion!.Entry<Empleados>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }


    }
}