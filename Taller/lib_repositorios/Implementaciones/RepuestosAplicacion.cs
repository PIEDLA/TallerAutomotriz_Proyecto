using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class RepuestosAplicacion : IRepuestosAplicacion
    {
        private IConexion? IConexion = null;

        public RepuestosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public List<Repuestos> Listar()
        {
            return this.IConexion!.Repuestos!
                .Take(20)
                .ToList();
        }

        public Repuestos? Guardar(Repuestos? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id != 0)
                throw new Exception("Ya se guardó");

            if (string.IsNullOrWhiteSpace(entidad.Nombre_repuesto))
                throw new Exception("El repuesto debe tener un nombre");

            if (string.IsNullOrWhiteSpace(entidad.Marca))
                throw new Exception("El repuesto debe tener una marca");

            if (entidad.Precio <= 0)
                throw new Exception("El precio debe ser mayor a cero");

            if (entidad.Stock < 0)
                throw new Exception("El stock no puede ser negativo");

            var proveedor = this.IConexion!.Proveedores!.Find(entidad!.Id_proveedor);
            proveedor!.Repuestos!.Add(entidad);

            this.IConexion!.Repuestos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Repuestos? Modificar(Repuestos? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id == 0)
                throw new Exception("No se guardó");

            if (string.IsNullOrWhiteSpace(entidad.Nombre_repuesto))
                throw new Exception("El repuesto debe tener un nombre");

            if (string.IsNullOrWhiteSpace(entidad.Marca))
                throw new Exception("El repuesto debe tener una marca");

            if (entidad.Precio <= 0)
                throw new Exception("El precio debe ser mayor a cero");

            if (entidad.Stock < 0)
                throw new Exception("El stock no puede ser negativo");

            var entry = this.IConexion!.Entry<Repuestos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Repuestos? Borrar(Repuestos? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            if (entidad.Id == 0)
                throw new Exception("No se guardó");

            if (entidad.Stock > 0)
                throw new Exception("No se puede borrar un repuesto con stock disponible");

            this.IConexion!.Repuestos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Repuestos> StockBajo(int limite = 5)
        {
            return this.IConexion!.Repuestos!
                .Where(r => r.Stock < limite)
                .ToList();
        }

        public List<Repuestos> PorMarca(string marca)
        {
            return this.IConexion!.Repuestos!
                .Where(r => r.Marca!.Contains(marca))
                .ToList();
        }
        public List<Repuestos> PorNombre(string Nombre)
        {
            return this.IConexion!.Repuestos!
                .Where(r => r.Nombre_repuesto!.Contains(Nombre))
                .ToList();
        }

        public List<Repuestos> PorProveedor(int idProveedor)
        {
            if (idProveedor == 0)
                return this.IConexion!.Repuestos!.Take(50).ToList();

            return this.IConexion!.Repuestos!
                .Where(r => r.Id_proveedor == idProveedor)
                .ToList();
        }

        public Repuestos? MasCaro()
        {
            return this.IConexion!.Repuestos!
                .OrderByDescending(r => r.Precio)
                .FirstOrDefault();
        }

        public Repuestos? MasBarato()
        {
            return this.IConexion!.Repuestos!
                .OrderBy(r => r.Precio)
                .FirstOrDefault();
        }

        public int StockTotal()
        {
            return this.IConexion!.Repuestos!
                .Sum(r => r.Stock);
        }
    }
}
