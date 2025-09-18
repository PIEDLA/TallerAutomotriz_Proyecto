using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IDiagnosticosAplicacion
    {
        void Configurar(string StringConexion);
        List<Diagnosticos> Listar();
        Diagnosticos? Guardar(Diagnosticos? entidad);
        Diagnosticos? Modificar(Diagnosticos? entidad);
        Diagnosticos? Borrar(Diagnosticos? entidad);

        List<Diagnosticos> PorVehiculo(int idVehiculo);
        List<Diagnosticos> PorEmpleado(int idEmpleado);
        List<Diagnosticos> PorRangoFechas(DateTime inicio, DateTime fin);
        int ContarPorVehiculo(int idVehiculo);
        Diagnosticos? UltimoDiagnostico();
    }
}
