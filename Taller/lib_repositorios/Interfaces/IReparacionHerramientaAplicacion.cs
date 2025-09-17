using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IReparacionHerramientaAplicacion
    {
        void Configurar(string StringConexion);
        List<Reparacion_Herramienta> Listar();
        Reparacion_Herramienta? Guardar(Reparacion_Herramienta? entidad);
        Reparacion_Herramienta? Modificar(Reparacion_Herramienta? entidad);
        Reparacion_Herramienta? Borrar(Reparacion_Herramienta? entidad);
    }
}
