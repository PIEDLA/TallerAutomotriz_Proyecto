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
            var lista = this.IConexion!.Herramientas!
                .Take(20)
                .ToList();

            // ✅ Limpiar referencias circulares
            foreach (var item in lista)
            {
                if (item.Reparacion_Herramienta != null)
                {
                    foreach (var rh in item.Reparacion_Herramienta)
                    {
                        rh._Herramienta = null;
                    }
                }
            }

            return lista;
        }

        public Herramientas? Guardar(Herramientas? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id != 0)
                throw new Exception("Ya se guardó");

            if (string.IsNullOrWhiteSpace(entidad.Nombre))
                throw new Exception("La herramienta debe tener un nombre");

            if (string.IsNullOrWhiteSpace(entidad.Tipo))
                throw new Exception("La herramienta debe tener un tipo");

            if (string.IsNullOrWhiteSpace(entidad.Estado))
                throw new Exception("La herramienta debe tener un estado");

            if (string.IsNullOrWhiteSpace(entidad.Ubicacion))
                throw new Exception("La herramienta debe tener una ubicación");

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

            if (string.IsNullOrWhiteSpace(entidad.Nombre))
                throw new Exception("La herramienta debe tener un nombre");

            if (string.IsNullOrWhiteSpace(entidad.Tipo))
                throw new Exception("La herramienta debe tener un tipo");

            if (string.IsNullOrWhiteSpace(entidad.Estado))
                throw new Exception("La herramienta debe tener un estado");

            if (string.IsNullOrWhiteSpace(entidad.Ubicacion))
                throw new Exception("La herramienta debe tener una ubicación");

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


            var herramientaConRelaciones = this.IConexion!.Herramientas!
                .Include(h => h.Reparacion_Herramienta)
                .FirstOrDefault(h => h.Id == entidad.Id);

            if (herramientaConRelaciones == null)
                throw new Exception("La herramienta no fue encontrada");


            if (herramientaConRelaciones.Reparacion_Herramienta != null &&
                herramientaConRelaciones.Reparacion_Herramienta.Any())
            {
                throw new Exception("No se puede borrar la herramienta porque está asociada a reparaciones");
            }

            this.IConexion.Herramientas!.Remove(herramientaConRelaciones);
            this.IConexion.SaveChanges();

            return herramientaConRelaciones;
        }

        public List<Herramientas> Disponibles()
        {
            var lista = this.IConexion!.Herramientas!
                .Where(h => h.Estado == "Disponible")
                .ToList();


            foreach (var item in lista)
            {
                if (item.Reparacion_Herramienta != null)
                {
                    foreach (var rh in item.Reparacion_Herramienta)
                    {
                        rh._Herramienta = null;
                    }
                }
            }

            return lista;
        }

        public List<Herramientas> EnMantenimiento()
        {
            var lista = this.IConexion!.Herramientas!
                .Where(h => h.Estado == "En reparación" || h.Estado == "Mantenimiento")
                .ToList();

            foreach (var item in lista)
            {
                if (item.Reparacion_Herramienta != null)
                {
                    foreach (var rh in item.Reparacion_Herramienta)
                    {
                        rh._Herramienta = null;
                    }
                }
            }

            return lista;
        }

        public List<Herramientas> PorTipo(string tipo)
        {
            var lista = this.IConexion!.Herramientas!
                .Where(h => h.Tipo!.Contains(tipo))
                .ToList();


            foreach (var item in lista)
            {
                if (item.Reparacion_Herramienta != null)
                {
                    foreach (var rh in item.Reparacion_Herramienta)
                    {
                        rh._Herramienta = null;
                    }
                }
            }

            return lista;
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