using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IProductosAplicacion
    {
        void Configurar(string StringConexion);
        List<Productos> PorEstudiante(Productos? entidad);
        List<Productos> Listar();
        Productos? Guardar(Productos? entidad);
        Productos? Modificar(Productos? entidad);
        Productos? Borrar(Productos? entidad);
    }
}