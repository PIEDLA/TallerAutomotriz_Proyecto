using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class ClientesAplicacion : IClientesAplicacion
    {
        private IConexion? IConexion = null;

        public ClientesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Clientes? Borrar(Clientes? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información del cliente");

            if (entidad!.Id == 0)
                throw new Exception("El cliente no existe");

            var tieneFacturas = this.IConexion!.Facturas!.Any(x => x.Id_cliente == entidad.Id);
            if (tieneFacturas)
            {
                throw new Exception("No se puede eliminar el cliente porque tiene facturas registradas.");
            }

            this.IConexion!.Clientes!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Clientes? Guardar(Clientes? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información del cliente");

            if (entidad.Id != 0)
                throw new Exception("El cliente ya existe");

            if (string.IsNullOrWhiteSpace(entidad.Nombre) || string.IsNullOrWhiteSpace(entidad.Apellido))
                throw new Exception("Nombre y apellido son obligatorios.");

            if (string.IsNullOrWhiteSpace(entidad.Telefono))
                throw new Exception("El teléfono es obligatorio.");

            if (string.IsNullOrWhiteSpace(entidad.Correo))
                throw new Exception("El correo es obligatorio.");

            this.IConexion!.Clientes!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Clientes> Listar()
        {
            return this.IConexion!.Clientes!
                        .Include(c => c.Vehiculos)
                        .Take(50)
                        .ToList();
        }

        public List<Clientes> PorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("Debe especificar un nombre para buscar");

            return this.IConexion!.Clientes!
                .Where(x => x.Nombre!.ToLower().Contains(nombre.ToLower()) || x.Apellido!.ToLower().Contains(nombre.ToLower()))
                .OrderBy(x => x.Apellido).ThenBy(x => x.Nombre).ToList();
        }



        public Clientes? Modificar(Clientes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            if (string.IsNullOrWhiteSpace(entidad.Nombre) || string.IsNullOrWhiteSpace(entidad.Apellido))
                throw new Exception("Nombre y apellido son obligatorios.");

            if (string.IsNullOrWhiteSpace(entidad.Telefono))
                throw new Exception("El teléfono es obligatorio.");

            if (string.IsNullOrWhiteSpace(entidad.Correo))
                throw new Exception("El correo es obligatorio.");

            var entry = this.IConexion!.Entry<Clientes>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

    }
 }
