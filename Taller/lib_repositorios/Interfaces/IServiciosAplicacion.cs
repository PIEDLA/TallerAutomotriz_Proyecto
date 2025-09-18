using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IServiciosAplicacion
    {
        void Configurar(string StringConexion);
        List<Servicios> Listar();
        Servicios? Guardar(Servicios? entidad);
        Servicios? Modificar(Servicios? entidad);
        Servicios? Borrar(Servicios? entidad);
    }
}