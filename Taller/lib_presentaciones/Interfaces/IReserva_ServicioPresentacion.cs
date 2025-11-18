using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IReserva_ServicioPresentacion
    {
        Task<List<Reserva_Servicio>> Listar();
        Task<List<Reserva_Servicio>> PorReserva(Reserva_Servicio? entidad);
        Task<List<Reserva_Servicio>> PorServicio(Reserva_Servicio? entidad);
        Task<Reserva_Servicio?> Guardar(Reserva_Servicio? entidad);
        Task<Reserva_Servicio?> Modificar(Reserva_Servicio? entidad);
        Task<Reserva_Servicio?> Borrar(Reserva_Servicio? entidad);
    }
}