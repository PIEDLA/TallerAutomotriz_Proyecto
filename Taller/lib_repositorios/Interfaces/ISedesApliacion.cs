using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface ISedesAplicacion
    {
        void Configurar(string StringConexion);
        List<Sedes> Listar();
        Sedes? Buscar(int Id);
        Sedes? Guardar(Sedes? entidad);
        Sedes? Modificar(Sedes? entidad);
        Sedes? Borrar(Sedes? entidad);
    }
}