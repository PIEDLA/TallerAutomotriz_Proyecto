using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IEmpleadosAplicacion
    {
        void Configurar(string StringConexion);
        List<Empleados> Listar();
        Empleados? Guardar(Empleados? entidad);
        Empleados? Modificar(Empleados? entidad);
        Empleados? Borrar(Empleados? entidad);
    }
}