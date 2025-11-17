using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IReservasAplicacion
    {
        void Configurar(string StringConexion);
        List<Reservas> Listar();
        List<Reservas> PorEstado(Reservas? entidad);
        Reservas? Guardar(Reservas? entidad);
        Reservas? Modificar(Reservas? entidad);
        Reservas? Borrar(Reservas? entidad);
    }
}