using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IHerramientasPresentacion
    {
        Task<List<Herramientas>> Listar();
        Task<Herramientas?> Guardar(Herramientas? entidad);
        Task<Herramientas?> Modificar(Herramientas? entidad);
        Task<Herramientas?> Borrar(Herramientas? entidad);

        Task<List<Herramientas>> Disponibles();
        Task<List<Herramientas>> EnMantenimiento();
        Task<List<Herramientas>> PorTipo(string tipo);

        Task<int> TotalHerramientas();
        Task<int> TotalDisponibles();
    }
}
