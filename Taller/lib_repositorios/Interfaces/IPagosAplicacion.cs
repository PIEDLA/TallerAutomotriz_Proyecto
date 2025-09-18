using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IPagosAplicacion
    {
        void Configurar(string StringConexion);
        List<Pagos> Listar();
        Pagos? Guardar(Pagos? entidad);
        Pagos? Modificar(Pagos? entidad);
        Pagos? Borrar(Pagos? entidad);

        List<Pagos> PorFactura(int idFactura);
        List<Pagos> PorEstado(string estado);
        List<Pagos> PorFecha(DateTime fecha);
        decimal TotalPagos();
        Pagos? UltimoPago();
    }
}
