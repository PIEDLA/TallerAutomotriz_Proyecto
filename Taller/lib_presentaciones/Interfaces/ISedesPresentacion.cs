using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface ISedesPresentacion
    {
        Task<List<Sedes>> Listar();
        Task<List<Sedes>> PorCiudad(Sedes? entidad);
        Task<Sedes?> Guardar(Sedes? entidad);
        Task<Sedes?> Modificar(Sedes? entidad);
        Task<Sedes?> Borrar(Sedes? entidad);
    }
}