using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IVehiculosAplicacion
    {
        void Configurar(string StringConexion);
        List<Vehiculos> Listar();
        Vehiculos? Guardar(Vehiculos? entidad);
        Vehiculos? Modificar(Vehiculos? entidad);
        Vehiculos? Borrar(Vehiculos? entidad);
    }
}