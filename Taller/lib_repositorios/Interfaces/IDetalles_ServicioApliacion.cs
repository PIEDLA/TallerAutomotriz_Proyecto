using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IDetalles_ServicioAplicacion
    {
        void Configurar(string StringConexion);
        List<Detalles_Servicio> PorEstudiante(Detalles_Servicio? entidad);
        List<Detalles_Servicio> Listar();
        Detalles_Servicio? Guardar(Detalles_Servicio? entidad);
        Detalles_Servicio? Modificar(Detalles_Servicio? entidad);
        Detalles_Servicio? Borrar(Detalles_Servicio? entidad);
    }
}