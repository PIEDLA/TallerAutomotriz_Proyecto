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
            var lista = this.IConexion!.Reparaciones!
                .Include(r => r._Diagnostico)
                .Take(10)
                .ToList();

            foreach (var item in lista)
            {
                if (item._Diagnostico != null)
                    item._Diagnostico.Reparaciones = null;

                if (item.Facturas != null)
                {
                    foreach (var factura in item.Facturas)
                    {
                        if (factura != null)
                            factura._Reparacion = null;
                    }
                }

                if (item.Reparacion_Herramienta != null)
                {
                    foreach (var rh in item.Reparacion_Herramienta)
                    {
                        if (rh != null)
                            rh._Reparacion = null;
                    }
                }
            }

            return lista;
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

            // Verificar que el diagnóstico existe e inicializar la lista si es null
            var diagnostico = this.IConexion!.Diagnosticos!.Find(entidad!.Id_diagnostico);

            if (diagnostico == null)
                throw new Exception("El diagnóstico no existe");

            if (diagnostico.Reparaciones == null)
            {
                diagnostico.Reparaciones = new List<Reparaciones>();
            }

            diagnostico.Reparaciones.Add(entidad);

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

            //  Cargar la reparación con sus relaciones
            var reparacionOriginal = this.IConexion!.Reparaciones!
                .Include(r => r.Facturas)
                .Include(r => r.Reparacion_Herramienta)
                .FirstOrDefault(r => r.Id == entidad.Id);

            if (reparacionOriginal == null)
                throw new Exception("La reparación no fue encontrada");

            //  Verificar si tiene facturas asociadas
            var tieneFacturas = reparacionOriginal.Facturas?.Any() ?? false;
            if (tieneFacturas)
            {
                throw new Exception("No se puede borrar la reparación porque tiene facturas asociadas");
            }

            // Verificar si tiene herramientas asociadas
            var tieneHerramientas = reparacionOriginal.Reparacion_Herramienta?.Any() ?? false;
            if (tieneHerramientas)
            {
                throw new Exception("No se puede borrar la reparación porque tiene herramientas asociadas");
            }

            reparacionOriginal._Diagnostico = null;

            this.IConexion.Reparaciones!.Remove(reparacionOriginal);
            this.IConexion.SaveChanges();

            return reparacionOriginal;
        }

        public List<Reparaciones> PorDiagnostico(int idDiagnostico)
        {
            var lista = this.IConexion!.Reparaciones!
                .Where(r => r.Id_diagnostico == idDiagnostico)
                .ToList();


            foreach (var item in lista)
            {
                item._Diagnostico = null;

                if (item.Facturas != null)
                {
                    foreach (var factura in item.Facturas)
                    {
                        if (factura != null)
                            factura._Reparacion = null;
                    }
                }

                if (item.Reparacion_Herramienta != null)
                {
                    foreach (var rh in item.Reparacion_Herramienta)
                    {
                        if (rh != null)
                            rh._Reparacion = null;
                    }
                }
            }

            return lista;
        }

        public List<Reparaciones> Costosas(decimal minimo)
        {
            var lista = this.IConexion!.Reparaciones!
                .Where(r => r.Costo_estimado >= minimo)
                .OrderByDescending(r => r.Costo_estimado)
                .ToList();


            foreach (var item in lista)
            {
                item._Diagnostico = null;

                if (item.Facturas != null)
                {
                    foreach (var factura in item.Facturas)
                    {
                        if (factura != null)
                            factura._Reparacion = null;
                    }
                }

                if (item.Reparacion_Herramienta != null)
                {
                    foreach (var rh in item.Reparacion_Herramienta)
                    {
                        if (rh != null)
                            rh._Reparacion = null;
                    }
                }
            }

            return lista;
        }

        public List<Reparaciones> EntreFechas(DateTime inicio, DateTime fin)
        {
            var lista = this.IConexion!.Reparaciones!
                .Where(r => r.Fecha_inicio >= inicio && r.Fecha_inicio <= fin)
                .ToList();


            foreach (var item in lista)
            {
                item._Diagnostico = null;

                if (item.Facturas != null)
                {
                    foreach (var factura in item.Facturas)
                    {
                        if (factura != null)
                            factura._Reparacion = null;
                    }
                }

                if (item.Reparacion_Herramienta != null)
                {
                    foreach (var rh in item.Reparacion_Herramienta)
                    {
                        if (rh != null)
                            rh._Reparacion = null;
                    }
                }
            }

            return lista;
        }

        public Reparaciones? UltimaReparacion()
        {
            var reparacion = this.IConexion!.Reparaciones!
                .OrderByDescending(r => r.Fecha_inicio)
                .FirstOrDefault();


            if (reparacion != null)
            {
                reparacion._Diagnostico = null;

                if (reparacion.Facturas != null)
                {
                    foreach (var factura in reparacion.Facturas)
                    {
                        if (factura != null)
                            factura._Reparacion = null;
                    }
                }

                if (reparacion.Reparacion_Herramienta != null)
                {
                    foreach (var rh in reparacion.Reparacion_Herramienta)
                    {
                        if (rh != null)
                            rh._Reparacion = null;
                    }
                }
            }

            return reparacion;
        }

        public decimal TotalEstimado()
        {
            return this.IConexion!.Reparaciones!
                .Sum(r => r.Costo_estimado);
        }
    }
}