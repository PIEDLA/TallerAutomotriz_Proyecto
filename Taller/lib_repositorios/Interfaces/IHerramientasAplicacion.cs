using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IHerramientasAplicacion
    {
        void Configurar(string StringConexion);
        List<Herramientas> Listar();
        Herramientas? Guardar(Herramientas? entidad);
        Herramientas? Modificar(Herramientas? entidad);
        Herramientas? Borrar(Herramientas? entidad);
    }
}
