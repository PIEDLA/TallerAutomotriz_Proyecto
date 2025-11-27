using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IReparacion_HerramientaPresentacion
    {
        Task<List<Reparacion_Herramienta>> Listar();
        Task<Reparacion_Herramienta?> Guardar(Reparacion_Herramienta? entidad);
        Task<Reparacion_Herramienta?> Modificar(Reparacion_Herramienta? entidad);
        Task<Reparacion_Herramienta?> Borrar(Reparacion_Herramienta? entidad);

        Task<List<Herramientas>> HerramientasPorReparacion(int idReparacion);
        Task<List<Reparaciones>> ReparacionesPorHerramienta(int idHerramienta);
        Task<int> VecesUsadaHerramienta(int idHerramienta);
    }
}
