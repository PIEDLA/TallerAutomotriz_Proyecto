using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IDetalles_RepuestoPresentacion
    {
        Task<List<Detalles_Repuesto>> Listar();
        Task<List<Detalles_Repuesto>> PorRepuesto(Detalles_Repuesto? entidad);
        Task<List<Detalles_Repuesto>> PorFactura(Detalles_Repuesto? entidad);
        Task<Detalles_Repuesto?> Guardar(Detalles_Repuesto? entidad);
        Task<Detalles_Repuesto?> Modificar(Detalles_Repuesto? entidad);
        Task<Detalles_Repuesto?> Borrar(Detalles_Repuesto? entidad);
    }
}