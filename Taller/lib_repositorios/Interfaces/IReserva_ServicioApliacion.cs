using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IReserva_ServicioAplicacion
    {
        void Configurar(string StringConexion);
        List<Reserva_Servicio> Listar();
        Reserva_Servicio? Buscar(int Id);
        Reserva_Servicio? Guardar(Reserva_Servicio? entidad);
        Reserva_Servicio? Modificar(Reserva_Servicio? entidad);
        Reserva_Servicio? Borrar(Reserva_Servicio? entidad);
    }
}