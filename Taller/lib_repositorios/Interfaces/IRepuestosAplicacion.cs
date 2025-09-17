using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IRepuestosAplicacion
    {
        void Configurar(string StringConexion);
        List<Repuestos> Listar();
        Repuestos? Guardar(Repuestos? entidad);
        Repuestos? Modificar(Repuestos? entidad);
        Repuestos? Borrar(Repuestos? entidad);
    }
}
