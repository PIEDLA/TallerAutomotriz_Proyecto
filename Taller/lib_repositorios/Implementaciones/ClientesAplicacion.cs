using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public List<Clientes> Listar()
        {
            return this.IConexion!.Clientes!.Take(50).ToList();
        }

        public Clientes? Guardar(Clientes? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información del cliente.");

            if (entidad.Id != 0)
                throw new Exception("El cliente ya fue guardado.");

            if (string.IsNullOrWhiteSpace(entidad.Nombre) || string.IsNullOrWhiteSpace(entidad.Apellido))
                throw new Exception("Nombre y apellido son obligatorios.");

            if (string.IsNullOrWhiteSpace(entidad.Telefono))
                throw new Exception("El teléfono es obligatorio.");

            if (string.IsNullOrWhiteSpace(entidad.Correo))
                throw new Exception("El correo es obligatorio.");

            this.IConexion.Clientes!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Clientes? Modificar(Clientes? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información del cliente.");

            if (entidad.Id == 0)
                throw new Exception("El cliente no está guardado.");

            var original = this.IConexion!.Clientes!.FirstOrDefault(c => c.Id == entidad.Id);

            if (original == null)
                throw new Exception("Cliente no encontrado.");

            if (string.IsNullOrWhiteSpace(entidad.Nombre) || string.IsNullOrWhiteSpace(entidad.Apellido))
                throw new Exception("Nombre y apellido son obligatorios.");

            if (string.IsNullOrWhiteSpace(entidad.Telefono))
                throw new Exception("El teléfono es obligatorio.");

            if (string.IsNullOrWhiteSpace(entidad.Correo))
                throw new Exception("El correo es obligatorio.");

            var entry = this.IConexion.Entry<Clientes>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Clientes? Borrar(Clientes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            bool tieneVehiculos = this.IConexion.Vehiculos!.Any(v => v.Id_cliente == entidad.Id);
            bool tieneReservas = this.IConexion.Reservas!.Any(r => r.Id_cliente == entidad.Id);
            bool tieneFacturas = this.IConexion.Facturas!.Any(f => f.Id_cliente == entidad.Id);

            if (tieneVehiculos || tieneReservas || tieneFacturas)
                throw new Exception("No se puede borrar el cliente porque tiene registros asociados (vehículos, reservas o facturas).");

            this.IConexion.Clientes!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Clientes> BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return new List<Clientes>();

            nombre = nombre.Trim();
            return this.IConexion!.Clientes!
                .Where(c => (c.Nombre + " " + c.Apellido).Contains(nombre))
                .Take(50)
                .ToList();
        }

        public Clientes? PorCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo)) return null;
            correo = correo.Trim().ToLowerInvariant();
            return this.IConexion!.Clientes!.FirstOrDefault(c => c.Correo!.ToLower() == correo);
        }

        public Clientes? PorTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono)) return null;
            telefono = telefono.Trim();
            return this.IConexion!.Clientes!.FirstOrDefault(c => c.Telefono == telefono);
        }
    }
}
