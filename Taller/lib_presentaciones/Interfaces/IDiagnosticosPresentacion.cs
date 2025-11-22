using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IDiagnosticosPresentacion
    {
        Task<List<Diagnosticos>> Listar();
        Task<List<Diagnosticos>> PorVehiculo(int idVehiculo);
        Task<List<Diagnosticos>> PorEmpleado(int idEmpleado);
        Task<List<Diagnosticos>> PorRangoFechas(DateTime inicio, DateTime fin);

        Task<Diagnosticos?> Guardar(Diagnosticos? entidad);
        Task<Diagnosticos?> Modificar(Diagnosticos? entidad);
        Task<Diagnosticos?> Borrar(Diagnosticos? entidad);

        Task<int> ContarPorVehiculo(int idVehiculo);
        Task<Diagnosticos?> UltimoDiagnostico();
    }
}
