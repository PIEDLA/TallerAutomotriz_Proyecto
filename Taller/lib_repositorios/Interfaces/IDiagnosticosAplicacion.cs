using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IDiagnosticosAplicacion
    {
        void Configurar(string StringConexion);
        List<Diagnosticos> Listar();
        Diagnosticos? Guardar(Diagnosticos? entidad);
        Diagnosticos? Modificar(Diagnosticos? entidad);
        Diagnosticos? Borrar(Diagnosticos? entidad);
    }
}
