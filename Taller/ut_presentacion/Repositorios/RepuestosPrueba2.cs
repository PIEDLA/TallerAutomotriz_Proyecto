using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class RepuestosPrueba2
    {
        private readonly IConexion? iConexion;
        private List<Repuestos>? lista;
        private Repuestos? entidad;

        public RepuestosPrueba2()
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
            this.lista = this.iConexion!.Repuestos!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = new Repuestos
            {
                Id_proveedor = 2,
                Nombre_repuesto = "Bujía",
                Marca = "NGK",
                Precio = 15.75m,
                Stock = 50
            };
            this.iConexion!.Repuestos!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Precio = 18.00m;
            var entry = this.iConexion!.Entry<Repuestos>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Repuestos!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}
