
﻿//using lib_dominio.Entidades;
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
        public DbSet<Detalles_Producto>? Detalles_Producto { get; set; }
        public DbSet<Detalles_Repuesto>? Detalles_Repuesto { get; set; }
        public DbSet<Detalles_Servicio>? Detalles_Servicio { get; set; }
        public DbSet<Reserva_Servicio>? Reserva_Servicio { get; set; }
        public DbSet<Detalles_PagoAplicacion>? Detalles_Pago { get; set; }
        public DbSet<Productos>? Productos { get; set; }
        public DbSet<Reservas>? Reservas { get; set; }
        public DbSet<Sedes>? Sedes { get; set; }
        public DbSet<Clientes>? Clientes { get; set; }
        public DbSet<Empleados>? Empleados { get; set; }
        public DbSet<Facturas>? Facturas { get; set; }
        public DbSet<Proveedores>? Proveedores { get; set; }
        public DbSet<Servicios>? Servicios { get; set; }
        public DbSet<Vehiculos>? Vehiculos { get; set; }
    }
}