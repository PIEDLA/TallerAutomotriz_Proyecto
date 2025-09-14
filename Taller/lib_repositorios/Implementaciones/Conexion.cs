//using lib_dominio.Entidades;
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
        public DbSet<Diagnosticos>? Diagnosticos { get; set; }
        public DbSet<Reparaciones>? Reparaciones { get; set; }
        public DbSet<Pagos>? Pagos { get; set; }
        public DbSet<Herramientas>? Herramientas { get; set; }
        public DbSet<Repuestos>? Repuestos { get; set; }
        public DbSet<Reparacion_Herramienta>? Reparacion_Herramienta { get; set; }
    }
}