using lib_dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace lib_repositorios.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

        DbSet<Clientes>? Clientes { get; set; }
        DbSet<Empleados>? Empleados { get; set; }
        DbSet<Facturas>? Facturas { get; set; }
        DbSet<Proveedores>? Proveedores { get; set; }
        DbSet<Servicios>? Servicios { get; set; }
        DbSet<Vehiculos>? Vehiculos { get; set; }
      
        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
