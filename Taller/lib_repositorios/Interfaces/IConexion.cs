//using lib_dominio.Entidades;
using lib_dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace lib_repositorios.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }
        DbSet<Diagnosticos>? Diagnosticos { get; set; }
        DbSet<Reparaciones>? Reparaciones { get; set; }
        DbSet<Pagos>? Pagos { get; set; }
        DbSet<Herramientas>? Herramientas { get; set; }
        DbSet<Repuestos>? Repuestos { get; set; }
        DbSet<Reparacion_Herramienta>? Reparacion_Herramienta { get; set; }
        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
