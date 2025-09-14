using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace lib_repositorios.Implementaciones
{
    public class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        public DbSet<Detalle_Factura>? Detalle_Factura { get; set; }
        public DbSet<Detalles_Pago>? Detalles_Pago { get; set; }
        public DbSet<Productos>? Productos { get; set; }
        public DbSet<Reservas>? Reservas { get; set; }
        public DbSet<Sedes>? Sedes { get; set; }
    }
}