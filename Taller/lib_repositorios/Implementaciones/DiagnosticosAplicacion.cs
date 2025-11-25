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
            var lista = this.IConexion!.Diagnosticos!
                .Include(x => x._Vehiculo)
                .Include(x => x._Empleado)
                .Take(20)
                .ToList();


            foreach (var item in lista)
            {
                if (item._Vehiculo != null)
                    item._Vehiculo.Diagnosticos = null;
                if (item._Empleado != null)
                {
                    var prop = item._Empleado.GetType().GetProperty("Diagnosticos");
                    if (prop != null)
                        prop.SetValue(item._Empleado, null);
                }
            }

            return lista;
        }


        public Diagnosticos? Guardar(Diagnosticos? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id != 0)
                throw new Exception("El diagnóstico ya existe");

            // Regla 1: el vehículo debe existir
            if (!this.IConexion!.Vehiculos!.Any(v => v.Id == entidad.Id_vehiculo))
                throw new Exception("El vehículo no existe");

            // Regla 2: el empleado debe existir
            if (!this.IConexion.Empleados!.Any(e => e.Id == entidad.Id_empleado))
                throw new Exception("El empleado no existe");

            // Regla 3: la descripción no puede estar vacía
            if (string.IsNullOrWhiteSpace(entidad.Descripcion))
                throw new Exception("Debe ingresar una descripción del diagnóstico");

            // Regla 4: la fecha no puede ser futura
            if (entidad.Fecha > DateTime.Now)
                throw new Exception("La fecha no puede ser futura");


            entidad._Vehiculo = null;
            entidad._Empleado = null;

            var vehiculo = this.IConexion!.Vehiculos!.Find(entidad!.Id_vehiculo);
            if (vehiculo == null)
                throw new Exception("El vehículo no fue encontrado al intentar asociar el diagnóstico.");

            if (vehiculo.Diagnosticos == null)
            {
                vehiculo.Diagnosticos = new List<Diagnosticos>();
            }
            vehiculo!.Diagnosticos!.Add(entidad);

            this.IConexion.Diagnosticos!.Add(entidad);
            this.IConexion.SaveChanges();

            entidad._Vehiculo = null;
            entidad._Empleado = null;

            return entidad;
        }

        public Diagnosticos? Modificar(Diagnosticos? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id == 0)
                throw new Exception("No se puede modificar un diagnóstico que no existe");


            var original = this.IConexion!.Diagnosticos!
                .Include(d => d.Reparaciones)
                .FirstOrDefault(d => d.Id == entidad.Id);

            if (original == null)
                throw new Exception("El diagnóstico no fue encontrado");

            // Regla: no se puede modificar un diagnóstico si ya tiene reparaciones asociadas
            if (original.Reparaciones != null && original.Reparaciones.Any())
                throw new Exception("No se puede modificar un diagnóstico con reparaciones asociadas");

            if (!this.IConexion.Vehiculos!.Any(v => v.Id == entidad.Id_vehiculo))
                throw new Exception("El vehículo no existe");

            if (!this.IConexion.Empleados!.Any(e => e.Id == entidad.Id_empleado))
                throw new Exception("El empleado no existe");

            if (string.IsNullOrWhiteSpace(entidad.Descripcion))
                throw new Exception("Debe ingresar una descripción del diagnóstico");

            if (entidad.Fecha > DateTime.Now)
                throw new Exception("La fecha no puede ser futura");

            entidad._Vehiculo = null;
            entidad._Empleado = null;

            var entry = this.IConexion.Entry<Diagnosticos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();

            return entidad;
        }

        public Diagnosticos? Borrar(Diagnosticos? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id == 0)
                throw new Exception("No se puede borrar un diagnóstico que no existe");

            var original = this.IConexion!.Diagnosticos!
                .Include(d => d.Reparaciones)
                .FirstOrDefault(d => d.Id == entidad.Id);

            if (original == null)
                throw new Exception("El diagnóstico no fue encontrado");

            // Regla: no se puede borrar un diagnóstico si ya tiene reparaciones
            if (original.Reparaciones != null && original.Reparaciones.Any())
                throw new Exception("No se puede borrar un diagnóstico con reparaciones asociadas");

            original._Vehiculo = null;
            original._Empleado = null;

            this.IConexion.Diagnosticos!.Remove(original);
            this.IConexion.SaveChanges();
            return original;
        }

        public List<Diagnosticos> PorVehiculo(int idVehiculo)
        {
            var lista = this.IConexion!.Diagnosticos!
                .Where(x => x.Id_vehiculo == idVehiculo)
                .ToList();


            foreach (var item in lista)
            {
                item._Vehiculo = null;
                item._Empleado = null;
            }

            return lista;
        }

        public List<Diagnosticos> PorEmpleado(int idEmpleado)
        {
            var lista = this.IConexion!.Diagnosticos!
                .Where(x => x.Id_empleado == idEmpleado)
                .ToList();


            foreach (var item in lista)
            {
                item._Vehiculo = null;
                item._Empleado = null;
            }

            return lista;
        }

        public List<Diagnosticos> PorRangoFechas(DateTime inicio, DateTime fin)
        {
            var lista = this.IConexion!.Diagnosticos!
                .Where(x => x.Fecha >= inicio && x.Fecha <= fin)
                .ToList();


            foreach (var item in lista)
            {
                item._Vehiculo = null;
                item._Empleado = null;
            }

            return lista;
        }

        public int ContarPorVehiculo(int idVehiculo)
        {
            return this.IConexion!.Diagnosticos!
                .Count(x => x.Id_vehiculo == idVehiculo);
        }

        public Diagnosticos? UltimoDiagnostico()
        {
            var diagnostico = this.IConexion!.Diagnosticos!
                .OrderByDescending(x => x.Fecha)
                .FirstOrDefault();


            if (diagnostico != null)
            {
                diagnostico._Vehiculo = null;
                diagnostico._Empleado = null;
            }

            return diagnostico;
        }
    }
}