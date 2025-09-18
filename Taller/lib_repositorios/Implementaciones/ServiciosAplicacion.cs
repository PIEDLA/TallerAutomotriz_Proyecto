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

            // Operaciones

            var servicioExistente = this.IConexion!.Servicios!.FirstOrDefault(x => x.Id == entidad.Id);

            if (servicioExistente == null)
                throw new Exception("El servicio que intenta eliminar no existe");

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

            // Operaciones

            this.IConexion!.Servicios!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Servicios> Listar()
        {
            return this.IConexion!.Servicios!.ToList();
        }

        public List<Servicios> BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("Debe especificar un nombre para buscar");

            return this.IConexion!.Servicios!.Where(x => x.Nombre_servicio!.ToLower().Contains(nombre.ToLower()) ||
                                                            x.Descripcion!.ToLower().Contains(nombre.ToLower()))
                                                            .OrderBy(s => s.Nombre_servicio).ToList();
        }

        public Servicios? Modificar(Servicios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Operaciones

            var servicioExistente = this.IConexion!.Servicios!.FirstOrDefault(x => x.Id == entidad.Id);

            if (servicioExistente == null)
                throw new Exception("El servicio que intenta modificar no existe");

            var entry = this.IConexion!.Entry<Servicios>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}