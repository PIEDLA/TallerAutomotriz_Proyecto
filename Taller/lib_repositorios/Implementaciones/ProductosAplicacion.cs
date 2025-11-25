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
            if (string.IsNullOrWhiteSpace(entidad.Nombre_producto))
                return "Nombre de producto requerido";
            if (string.IsNullOrWhiteSpace(entidad.Categoria))
                return "Categoría de producto requerido";
            if (entidad.Precio <= 0)
                return "Precio del producto requerido";
            if (entidad.Stock < 0)
                return "No puede haber stock negativa";
            if (string.IsNullOrWhiteSpace(entidad.Imagen_Base64)) 
                return "Se requiere la imagen del producto";
            return null;
        }

        public Productos? Borrar(Productos? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");
            if (entidad!.Id == 0)
                throw new Exception("Producto no guardado");
            if (entidad.Stock > 0)
                throw new Exception("Aún existe stock del producto");

            this.IConexion!.Productos!.Remove(entidad);
            this.IConexion.SaveChanges();

            return entidad;
        }

        public Productos? Guardar(Productos? entidad)
        {
            if (entidad == null)
                throw new Exception("Información incompleta");
            if (entidad!.Id != 0)
                throw new Exception("Producto ya guardado. Use Modificar.");

            var v = Validar(entidad!);
            if (v != null)
                throw new Exception(v);

            this.IConexion!.Productos!.Add(entidad);
            this.IConexion.SaveChanges();


            if (entidad.detalles_Productos != null)
            {
                foreach (var detalle in entidad.detalles_Productos)
                {
                    var prop = detalle.GetType().GetProperty("_Producto");
                    if (prop != null)
                        prop.SetValue(detalle, null);
                }
            }

            return entidad;
        }

        public List<Productos> Listar()
        {
            var lista = this.IConexion!.Productos!.Take(50).ToList();


            foreach (var item in lista)
            {
                if (item.detalles_Productos != null)
                {
                    foreach (var detalle in item.detalles_Productos)
                    {
                        var prop = detalle.GetType().GetProperty("_Producto");
                        if (prop != null)
                            prop.SetValue(detalle, null);
                    }
                }
            }

            return lista;
        }

        public List<Productos> PorCategoria(Productos? entidad)
        {
            var lista = this.IConexion!.Productos!
                .Where(x => x.Categoria!.Contains(entidad!.Categoria!))
                .Take(50)
                .ToList();


            foreach (var item in lista)
            {
                if (item.detalles_Productos != null)
                {
                    foreach (var detalle in item.detalles_Productos)
                    {
                        var prop = detalle.GetType().GetProperty("_Producto");
                        if (prop != null)
                            prop.SetValue(detalle, null);
                    }
                }
            }

            return lista;
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


            if (entidad.detalles_Productos != null)
            {
                foreach (var detalle in entidad.detalles_Productos)
                {
                    var prop = detalle.GetType().GetProperty("_Producto");
                    if (prop != null)
                        prop.SetValue(detalle, null);
                }
            }

            return entidad;
        }
    }
}