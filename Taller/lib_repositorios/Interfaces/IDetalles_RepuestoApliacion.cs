using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IDetalles_RepuestoAplicacion
    {
        void Configurar(string StringConexion);
        List<Detalles_Repuesto> Listar();
        Detalles_Repuesto? Buscar(int Id);
        Detalles_Repuesto? Guardar(Detalles_Repuesto? entidad);
        Detalles_Repuesto? Modificar(Detalles_Repuesto? entidad);
        Detalles_Repuesto? Borrar(Detalles_Repuesto? entidad);
    }
}