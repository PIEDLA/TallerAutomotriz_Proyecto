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
                .Take(5)
                .ToList();
        }

        public Reparacion_Herramienta? Guardar(Reparacion_Herramienta? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id != 0)
                throw new Exception("Ya se guardó");

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

        public List<Reparacion_Herramienta> PorSede(int idSede)
        {
            return this.IConexion!.Reparacion_Herramienta!
                .Where(rh => rh._Reparacion!._Diagnostico!._Vehiculo!.Id_cliente == idSede)
                .ToList();
        }
    }
}
