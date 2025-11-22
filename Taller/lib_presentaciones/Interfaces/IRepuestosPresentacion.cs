using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IRepuestosPresentacion
    {
        Task<List<Repuestos>> Listar();
        Task<Repuestos?> Guardar(Repuestos? entidad);
        Task<Repuestos?> Modificar(Repuestos? entidad);
        Task<Repuestos?> Borrar(Repuestos? entidad);

        Task<List<Repuestos>> StockBajo(int limite = 5);
        Task<List<Repuestos>> PorMarca(string marca);
        Task<List<Repuestos>> PorProveedor(int idProveedor);
        Task<Repuestos?> MasCaro();
        Task<Repuestos?> MasBarato();
        Task<int> StockTotal();
    }
}
