using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class ReparacionesAplicacion : IReparacionesAplicacion
    {
        private IConexion? IConexion = null;

        public ReparacionesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public List<Reparaciones> Listar()
        {
            return this.IConexion!.Reparaciones!
                .Include(r => r._Diagnostico)
                .Take(10)
                .ToList();
        }

        public Reparaciones? Guardar(Reparaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información de la reparación");

            if (entidad.Id != 0)
                throw new Exception("La reparación ya existe");

            if (entidad.Id_diagnostico <= 0)
                throw new Exception("Debe estar asociada a un diagnóstico válido");

            if (string.IsNullOrWhiteSpace(entidad.Descripcion_trabajo))
                throw new Exception("Debe describir el trabajo a realizar");

            if (entidad.Costo_estimado <= 0)
                throw new Exception("El costo estimado debe ser mayor a 0");

            if (entidad.Fecha_inicio > DateTime.Now)
                throw new Exception("La fecha de inicio no puede ser futura");

            entidad._Diagnostico = null;

            var diagnostico = this.IConexion!.Diagnosticos!.Find(entidad!.Id_diagnostico);
            diagnostico!.Reparaciones!.Add(entidad);

            this.IConexion!.Reparaciones!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Reparaciones? Modificar(Reparaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información de la reparación");

            if (entidad.Id == 0)
                throw new Exception("La reparación no existe");

            if (string.IsNullOrWhiteSpace(entidad.Descripcion_trabajo))
                throw new Exception("Debe describir el trabajo");

            if (entidad.Costo_estimado <= 0)
                throw new Exception("El costo estimado debe ser mayor a 0");

            entidad._Diagnostico = null;

            var entry = this.IConexion!.Entry<Reparaciones>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Reparaciones? Borrar(Reparaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información de la reparación");

            if (entidad.Id == 0)
                throw new Exception("La reparación no existe");

            entidad._Diagnostico = null;

            this.IConexion!.Reparaciones!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Reparaciones> PorDiagnostico(int idDiagnostico)
        {
            return this.IConexion!.Reparaciones!
                .Where(r => r.Id_diagnostico == idDiagnostico)
                .ToList();
        }

        public List<Reparaciones> Costosas(decimal minimo)
        {
            return this.IConexion!.Reparaciones!
                .Where(r => r.Costo_estimado >= minimo)
                .OrderByDescending(r => r.Costo_estimado)
                .ToList();
        }

        public List<Reparaciones> EntreFechas(DateTime inicio, DateTime fin)
        {
            return this.IConexion!.Reparaciones!
                .Where(r => r.Fecha_inicio >= inicio && r.Fecha_inicio <= fin)
                .ToList();
        }

        public Reparaciones? UltimaReparacion()
        {
            return this.IConexion!.Reparaciones!
                .OrderByDescending(r => r.Fecha_inicio)
                .FirstOrDefault();
        }

        public decimal TotalEstimado()
        {
            return this.IConexion!.Reparaciones!
                .Sum(r => r.Costo_estimado);
        }
    }
}
