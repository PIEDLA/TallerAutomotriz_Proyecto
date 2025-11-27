using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IReparacionesPresentacion
    {
        Task<List<Reparaciones>> Listar();
        Task<Reparaciones?> Guardar(Reparaciones entidad);
        Task<Reparaciones?> Modificar(Reparaciones entidad);
        Task<Reparaciones?> Borrar(Reparaciones entidad);

        Task<List<Reparaciones>> PorDiagnostico(int idDiagnostico);
        Task<List<Reparaciones>> Costosas(decimal minimo);
        Task<List<Reparaciones>> EntreFechas(DateTime inicio, DateTime fin);
        Task<Reparaciones?> UltimaReparacion();
        Task<decimal> TotalEstimado();
    }
}
