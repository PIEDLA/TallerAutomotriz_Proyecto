using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class ReparacionHerramientaAplicacion : IReparacionHerramientaAplicacion
    {
        private IConexion? IConexion = null;

        public ReparacionHerramientaAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public List<Reparacion_Herramienta> Listar()
        {
            var lista = this.IConexion!.Reparacion_Herramienta!
                .Include(rh => rh._Reparacion)
                .Include(rh => rh._Herramienta)
                .Take(10)
                .ToList();


            foreach (var item in lista)
            {
                if (item._Reparacion != null)
                {
                    item._Reparacion.Reparacion_Herramienta = null;
                    item._Reparacion._Diagnostico = null;
                    item._Reparacion.Facturas = null;
                }

                if (item._Herramienta != null)
                {
                    item._Herramienta.Reparacion_Herramienta = null;
                }
            }

            return lista;
        }

        public Reparacion_Herramienta? Guardar(Reparacion_Herramienta? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id != 0)
                throw new Exception("Ya se guardó");

            // Regla: una herramienta no puede asignarse dos veces a la misma reparación
            bool yaAsignada = this.IConexion!.Reparacion_Herramienta!
                .Any(rh => rh.Id_reparacion == entidad.Id_reparacion && rh.Id_herramienta == entidad.Id_herramienta);

            if (yaAsignada)
                throw new Exception("Esta herramienta ya fue asignada a esta reparación");

            // Regla: la herramienta debe estar disponible
            var herramienta = this.IConexion!.Herramientas!.FirstOrDefault(h => h.Id == entidad.Id_herramienta);
            if (herramienta == null)
                throw new Exception("La herramienta no existe");

            if (herramienta.Estado != "Disponible")
                throw new Exception("La herramienta no está disponible");

            entidad._Reparacion = null;
            entidad._Herramienta = null;


            var reparacion = this.IConexion!.Reparaciones!.Find(entidad!.Id_reparacion);
            if (reparacion != null)
            {
                if (reparacion.Reparacion_Herramienta == null)
                {
                    reparacion.Reparacion_Herramienta = new List<Reparacion_Herramienta>();
                }
                reparacion.Reparacion_Herramienta.Add(entidad);
            }

            var herramient = this.IConexion!.Herramientas!.Find(entidad!.Id_herramienta);
            if (herramient != null)
            {
                if (herramient.Reparacion_Herramienta == null)
                {
                    herramient.Reparacion_Herramienta = new List<Reparacion_Herramienta>();
                }
                herramient.Reparacion_Herramienta.Add(entidad);
            }

            this.IConexion!.Reparacion_Herramienta!.Add(entidad);
            this.IConexion.SaveChanges();

            return entidad;
        }

        public Reparacion_Herramienta? Modificar(Reparacion_Herramienta? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id == 0)
                throw new Exception("No se guardó");

            entidad._Reparacion = null;
            entidad._Herramienta = null;

            var entry = this.IConexion!.Entry<Reparacion_Herramienta>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();

            return entidad;
        }

        public Reparacion_Herramienta? Borrar(Reparacion_Herramienta? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id == 0)
                throw new Exception("No se guardó");

            entidad._Reparacion = null;
            entidad._Herramienta = null;

            this.IConexion!.Reparacion_Herramienta!.Remove(entidad);
            this.IConexion.SaveChanges();

            return entidad;
        }

        public List<Herramientas> HerramientasPorReparacion(int idReparacion)
        {
            var lista = this.IConexion!.Reparacion_Herramienta!
                .Where(rh => rh.Id_reparacion == idReparacion)
                .Select(rh => rh._Herramienta!)
                .ToList();


            foreach (var item in lista)
            {
                if (item != null)
                {
                    item.Reparacion_Herramienta = null;
                }
            }

            return lista;
        }

        public List<Reparaciones> ReparacionesPorHerramienta(int idHerramienta)
        {
            var lista = this.IConexion!.Reparacion_Herramienta!
                .Where(rh => rh.Id_herramienta == idHerramienta)
                .Select(rh => rh._Reparacion!)
                .ToList();

            foreach (var item in lista)
            {
                if (item != null)
                {
                    item._Diagnostico = null;
                    item.Reparacion_Herramienta = null;
                    item.Facturas = null;
                }
            }

            return lista;
        }

        public int VecesUsadaHerramienta(int idHerramienta)
        {
            return this.IConexion!.Reparacion_Herramienta!
                .Count(rh => rh.Id_herramienta == idHerramienta);
        }
    }
}