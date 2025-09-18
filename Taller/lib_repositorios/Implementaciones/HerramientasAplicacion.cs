using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class HerramientasAplicacion : IHerramientasAplicacion
    {
        private IConexion? IConexion = null;

        public HerramientasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }


        public List<Herramientas> Listar()
        {
            return this.IConexion!.Herramientas!
                .Take(20)
                .ToList();
        }

        public Herramientas? Guardar(Herramientas? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id != 0)
                throw new Exception("Ya se guardó");

            this.IConexion!.Herramientas!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Herramientas? Modificar(Herramientas? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id == 0)
                throw new Exception("No se guardó");

            var entry = this.IConexion!.Entry<Herramientas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Herramientas? Borrar(Herramientas? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id == 0)
                throw new Exception("No se guardó");

            this.IConexion!.Herramientas!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }


        public List<Herramientas> Disponibles()
        {
            return this.IConexion!.Herramientas!
                .Where(h => h.Estado == "Disponible")
                .ToList();
        }

        public List<Herramientas> EnMantenimiento()
        {
            return this.IConexion!.Herramientas!
                .Where(h => h.Estado == "Mantenimiento")
                .ToList();
        }

        public List<Herramientas> PorTipo(string tipo)
        {
            return this.IConexion!.Herramientas!
                .Where(h => h.Tipo.Contains(tipo))
                .ToList();
        }

        public int TotalHerramientas()
        {
            return this.IConexion!.Herramientas!.Count();
        }

        public int TotalDisponibles()
        {
            return this.IConexion!.Herramientas!
                .Count(h => h.Estado == "Disponible");
        }
    }
}
