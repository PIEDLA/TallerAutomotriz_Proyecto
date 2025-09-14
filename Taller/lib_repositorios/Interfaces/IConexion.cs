using lib_dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace lib_repositorios.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

        DbSet<Detalle_Factura>? Detalle_Factura { get; set; }
        DbSet<Detalles_Pago>? Detalles_Pago { get; set; }
        DbSet<Productos>? Productos { get; set; }
        DbSet<Reservas>? Reservas { get; set; }
        DbSet<Sedes>? Sedes { get; set; }
        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
