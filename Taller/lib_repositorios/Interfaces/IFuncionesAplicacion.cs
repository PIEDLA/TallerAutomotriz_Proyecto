using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IFuncionesAplicacion
    {
        void Configurar(string StringConexion);
        List<Funciones> Listar();
        Funciones? Guardar(Funciones? entidad);
        Funciones? Modificar(Funciones? entidad);
        Funciones? Borrar(Funciones? entidad);
    }
}