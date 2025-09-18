using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class PagosPrueba2
    {
        private readonly IConexion? iConexion;
        private List<Pagos>? lista;
        private Pagos? entidad;

        public PagosPrueba2()
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
            this.lista = this.iConexion!.Pagos!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = new Pagos
            {
                Id_factura = 2,
                Monto_total = 750.50m,
                Fecha_pago = DateTime.Now,
                Estado = "Pendiente"
            };
            this.iConexion!.Pagos!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Monto_total = 800.00m;
            var entry = this.iConexion!.Entry<Pagos>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Pagos!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}
