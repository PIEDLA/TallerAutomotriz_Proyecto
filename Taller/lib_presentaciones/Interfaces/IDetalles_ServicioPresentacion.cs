using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IDetalles_ServicioPresentacion
    {
        Task<List<Detalles_Servicio>> Listar();
        Task<List<Detalles_Servicio>> PorServicio(Detalles_Servicio? entidad);
        Task<List<Detalles_Servicio>> PorFactura(Detalles_Servicio? entidad);
        Task<Detalles_Servicio?> Guardar(Detalles_Servicio? entidad);
        Task<Detalles_Servicio?> Modificar(Detalles_Servicio? entidad);
        Task<Detalles_Servicio?> Borrar(Detalles_Servicio? entidad);
    }
}