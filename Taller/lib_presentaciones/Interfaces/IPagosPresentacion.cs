using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IPagosPresentacion
    {
        Task<List<Pagos>> Listar();
        Task<Pagos?> Guardar(Pagos? entidad);
        Task<Pagos?> Modificar(Pagos? entidad);
        Task<Pagos?> Borrar(Pagos? entidad);

        Task<List<Pagos>> PorFecha(DateTime fecha);
        Task<List<Pagos>> PorFactura(int idFactura);
        Task<List<Pagos>> PorEstado(string estado);

        Task<decimal> TotalPagos();
        Task<Pagos?> UltimoPago();
    }
}
