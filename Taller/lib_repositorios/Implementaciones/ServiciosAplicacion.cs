using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class ServiciosAplicacion : IServiciosAplicacion
    {
        private IConexion? IConexion = null;

        public ServiciosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Servicios? Borrar(Servicios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");


            this.IConexion!.Servicios!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Servicios? Guardar(Servicios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            if (string.IsNullOrWhiteSpace(entidad.Nombre_servicio))
                throw new Exception("El servicio debe tener un nombre");

            if (entidad.Precio <= 0)
                throw new Exception("El precio del servicio debe ser mayor que 0");

            if (string.IsNullOrWhiteSpace(entidad.Duracion_aprox))
                throw new Exception("Debe especificar la duración aproximada del servicio");

            this.IConexion!.Servicios!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Servicios> ListarPorDuracion(string duracion)
        {
  
                return this.IConexion!.Servicios!
                    .Where(x => x.Duracion_aprox == duracion)
                    .ToList();
            
        }


        public List<Servicios> Listar()
        {
            return this.IConexion!.Servicios!.ToList();
        }

        public Servicios? Modificar(Servicios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            if (string.IsNullOrWhiteSpace(entidad.Nombre_servicio))
                throw new Exception("El servicio debe tener un nombre");

            if (entidad.Precio <= 0)
                throw new Exception("El precio del servicio debe ser mayor que 0");

            if (string.IsNullOrWhiteSpace(entidad.Duracion_aprox))
                throw new Exception("Debe especificar la duración aproximada del servicio");

            var entry = this.IConexion!.Entry<Servicios>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}