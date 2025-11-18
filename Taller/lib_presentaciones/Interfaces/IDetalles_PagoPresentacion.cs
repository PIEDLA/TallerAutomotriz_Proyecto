using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IDetalles_PagoPresentacion
    {
        Task<List<Detalles_Pago>> Listar();
        Task<List<Detalles_Pago>> PorMetodoPago(Detalles_Pago? entidad);
        Task<Detalles_Pago?> Guardar(Detalles_Pago? entidad);
        Task<Detalles_Pago?> Modificar(Detalles_Pago? entidad);
        Task<Detalles_Pago?> Borrar(Detalles_Pago? entidad);
    }
}