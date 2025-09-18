using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IReparacionesAplicacion
    {
        void Configurar(string StringConexion);
        List<Reparaciones> Listar();
        Reparaciones? Guardar(Reparaciones? entidad);
        Reparaciones? Modificar(Reparaciones? entidad);
        Reparaciones? Borrar(Reparaciones? entidad);

        List<Reparaciones> PorDiagnostico(int idDiagnostico);
        List<Reparaciones> Costosas(decimal minimo);
        List<Reparaciones> EntreFechas(DateTime inicio, DateTime fin);
        Reparaciones? UltimaReparacion();
        decimal TotalEstimado();
    }
}
