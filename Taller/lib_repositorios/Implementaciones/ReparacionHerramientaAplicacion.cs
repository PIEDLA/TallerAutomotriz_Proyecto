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
            return this.IConexion!.Reparacion_Herramienta!
                .Include(rh => rh._Reparacion)
                .Include(rh => rh._Herramienta)
                .Take(10)
                .ToList();
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
            return this.IConexion!.Reparacion_Herramienta!
                .Where(rh => rh.Id_reparacion == idReparacion)
                .Select(rh => rh._Herramienta!)
                .ToList();
        }

        public List<Reparaciones> ReparacionesPorHerramienta(int idHerramienta)
        {
            return this.IConexion!.Reparacion_Herramienta!
                .Where(rh => rh.Id_herramienta == idHerramienta)
                .Select(rh => rh._Reparacion!)
                .ToList();
        }

        public int VecesUsadaHerramienta(int idHerramienta)
        {
            return this.IConexion!.Reparacion_Herramienta!
                .Count(rh => rh.Id_herramienta == idHerramienta);
        }
    }
}
