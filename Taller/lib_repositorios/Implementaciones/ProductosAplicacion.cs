using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class ProductosAplicacion : IProductosAplicacion
    {
        private IConexion? IConexion = null;

        public ProductosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        private string? Validar(Productos entidad)
        {
            if (string.IsNullOrWhiteSpace(entidad.Nombre_producto)) return "Nombre de producto requerido";
            if (string.IsNullOrWhiteSpace(entidad.Categoria)) return "Categoría de producto requerido";
            if (entidad.Precio <= 0) return "Precio del producto requerido";
            if (entidad.Stock < 0) return "No puede haber stock negativa";

            return null;
        }

        public Productos? Borrar(Productos? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Producto no guardado");

            this.IConexion!.Productos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Productos? Guardar(Productos? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Producto no guardado");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            this.IConexion!.Productos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Productos> Listar()
        {
            return this.IConexion!.Productos!.Take(5).ToList();
        }

        public Productos? Buscar(int Id)
        {
            var entidad = this.IConexion!.Productos!.Find(Id);
            if (entidad == null)
                throw new Exception("Producto no existente");

            return entidad;
        }

        public Productos? Modificar(Productos? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");

            if (entidad!.Id == 0)
                throw new Exception("Producto no guardado");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            var entry = this.IConexion!.Entry<Productos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}