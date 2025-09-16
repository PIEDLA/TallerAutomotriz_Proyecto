﻿using lib_dominio.Entidades;
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
        DbSet<Detalle_Factura>? Detalle_Factura { get; set; }
        DbSet<Detalles_Pago>? Detalles_Pago { get; set; }
        DbSet<Productos>? Productos { get; set; }
        DbSet<Reservas>? Reservas { get; set; }
        DbSet<Sedes>? Sedes { get; set; }
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
