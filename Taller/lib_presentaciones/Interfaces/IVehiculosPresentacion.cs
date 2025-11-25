using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IVehiculosPresentacion
    {
        Task<List<Vehiculos>> Listar();
        Task<List<Vehiculos>> ListarPorCliente(int IdCliente);
        Task<Vehiculos?> Guardar(Vehiculos? entidad);
        Task<Vehiculos?> Modificar(Vehiculos? entidad);
        Task<Vehiculos?> Borrar(Vehiculos? entidad);
    }
}