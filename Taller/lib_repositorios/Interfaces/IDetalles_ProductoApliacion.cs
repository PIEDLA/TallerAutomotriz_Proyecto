using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IDetalles_ProductoAplicacion
    {
        void Configurar(string StringConexion);
        List<Detalles_Producto> PorEstudiante(Detalles_Producto? entidad);
        List<Detalles_Producto> Listar();
        Detalles_Producto? Guardar(Detalles_Producto? entidad);
        Detalles_Producto? Modificar(Detalles_Producto? entidad);
        Detalles_Producto? Borrar(Detalles_Producto? entidad);
    }
}