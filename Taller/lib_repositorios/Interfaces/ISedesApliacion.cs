using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface ISedesAplicacion
    {
        void Configurar(string StringConexion);
        List<Sedes> PorEstudiante(Sedes? entidad);
        List<Sedes> Listar();
        Sedes? Guardar(Sedes? entidad);
        Sedes? Modificar(Sedes? entidad);
        Sedes? Borrar(Sedes? entidad);
    }
}