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
    }
}
