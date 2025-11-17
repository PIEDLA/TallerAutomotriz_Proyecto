using lib_dominio.Entidades;
using System.Diagnostics.Metrics;

namespace lib_repositorios.Interfaces
{
    public interface IFacturasAplicacion
    {
        void Configurar(string StringConexion);
        List<Facturas> Listar();
        Facturas? Guardar(Facturas? entidad);
        Facturas? Modificar(Facturas? entidad);
        Facturas? Borrar(Facturas? entidad);
        public List<Facturas> ListarPorCliente(int idCliente);
    }
}