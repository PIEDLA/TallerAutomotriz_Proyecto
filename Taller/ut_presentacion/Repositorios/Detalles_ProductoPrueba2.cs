using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class Detalles_ProductoPrueba2
    {
        private readonly IConexion? iConexion;
        private List<Detalles_Producto>? lista;
        private Detalles_Producto? entidad;

        public Detalles_ProductoPrueba2()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }

        public bool Listar()
        {
            this.lista = this.iConexion!.Detalles_Producto!
            .Include(x => x._Factura)
            .Include(x => x._Producto)
            .ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Detalles_Producto()!;
            this.iConexion!.Detalles_Producto!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Producto = 3;
            this.entidad!.PrecioProducto = 3;
            this.entidad!.Factura = 3;

            var entry = this.iConexion!.Entry<Detalles_Producto>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Detalles_Producto!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}