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
                throw new Exception("Falta informacion del empleado");

            if (entidad!.Id == 0)
                throw new Exception("El empleado no existe");

            bool tieneDiagnosticos = this.IConexion!.Diagnosticos!.Any(d => d.Id_empleado == entidad.Id);
            if (tieneDiagnosticos)
                throw new Exception("No se puede eliminar un empleado con diagnósticos asociados");

            this.IConexion!.Empleados!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Empleados? Guardar(Empleados? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta informacion del empleado");

            if (entidad.Id != 0)
                throw new Exception("El empleado ya existe");

            if (string.IsNullOrWhiteSpace(entidad.Nombre) || string.IsNullOrWhiteSpace(entidad.Apellido))
                throw new Exception("Nombre y apellido son obligatorios.");

            if (string.IsNullOrWhiteSpace(entidad.Telefono))
                throw new Exception("El teléfono es obligatorio.");

            // Validar que la sede existe
            var sedeExiste = this.IConexion!.Sedes!.Any(s => s.Id == entidad.Id_sede);
            if (!sedeExiste)
                throw new Exception("La sede no existe");

            this.IConexion!.Empleados!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Empleados> Listar()
        {
            return this.IConexion!.Empleados!.ToList();
        }

        public List<Empleados> ListarPorSede(Empleados? entidad)
        {
            List<Empleados> lista;

            if (entidad!.Id_sede == 0)
            {
                lista = this.IConexion!.Empleados!
                    .Include(x => x._Sede)
                    .Take(50)
                    .ToList();
            }
            else
            {
                lista = this.IConexion!.Empleados!
                    .Include(x => x._Sede)
                    .Where(x => x.Id_sede == entidad.Id_sede)
                    .ToList();
            }


            foreach (var empleado in lista)
            {
                if (empleado._Sede != null)
                    empleado._Sede.empleados = null;
            }

            return lista;
        }

        public Empleados? Modificar(Empleados? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            if (string.IsNullOrWhiteSpace(entidad.Nombre) || string.IsNullOrWhiteSpace(entidad.Apellido))
                throw new Exception("Nombre y apellido son obligatorios.");

            if (string.IsNullOrWhiteSpace(entidad.Telefono))
                throw new Exception("El teléfono es obligatorio.");

            var entry = this.IConexion!.Entry<Empleados>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}