using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IDetalles_PagoAplicacion
    {
        void Configurar(string StringConexion);
        List<Detalles_Pago> Listar();
        Detalles_Pago? Buscar(int Id);
        Detalles_Pago? Guardar(Detalles_Pago? entidad);
        Detalles_Pago? Modificar(Detalles_Pago? entidad);
        Detalles_Pago? Borrar(Detalles_Pago? entidad);
    }
}