using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IProductosAplicacion
    {
        void Configurar(string StringConexion);
        List<Productos> Listar();
        List<Productos> PorCategoria(Productos? entidad);
        Productos? Guardar(Productos? entidad);
        Productos? Modificar(Productos? entidad);
        Productos? Borrar(Productos? entidad);
    }
}