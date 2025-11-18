using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IDetalles_ProductoPresentacion
    {
        Task<List<Detalles_Producto>> Listar();
        Task<List<Detalles_Producto>> PorProducto(Detalles_Producto? entidad);
        Task<List<Detalles_Producto>> PorFactura(Detalles_Producto? entidad);
        Task<Detalles_Producto?> Guardar(Detalles_Producto? entidad);
        Task<Detalles_Producto?> Modificar(Detalles_Producto? entidad);
        Task<Detalles_Producto?> Borrar(Detalles_Producto? entidad);
    }
}