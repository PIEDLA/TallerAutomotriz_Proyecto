using lib_dominio.Entidades;
using System.Diagnostics.Metrics;

namespace lib_repositorios.Interfaces
{
    public interface IClientesAplicacion
    {
        void Configurar(string StringConexion);
        List<Clientes> Listar();
        Clientes? Guardar(Clientes? entidad);
        Clientes? Modificar(Clientes? entidad);
        Clientes? Borrar(Clientes? entidad);
        public List<Clientes> PorNombre(string nombre);
        public List<Clientes> PorDocumento(Clientes? entidad);
    }
}