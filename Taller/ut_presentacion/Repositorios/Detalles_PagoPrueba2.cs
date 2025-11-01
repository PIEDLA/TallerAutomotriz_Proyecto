using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class Detalles_PagoPrueba2
    {
        private readonly IConexion? iConexion;
        private List<Detalles_Pago>? lista;
        private Detalles_Pago? entidad;

        public Detalles_PagoPrueba2()
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
            this.lista = this.iConexion!.Detalles_Pago!
            .Include(x => x._Pago)
            .ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Detalles_Pago()!;

            this.iConexion!.Detalles_Pago!.Add(this.entidad);
            this.iConexion!.SaveChanges();

            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Id_pago = 3;
            this.entidad!.Metodo_pago = "Prueba";
            this.entidad!.Monto = 165714.0m;
            this.entidad!.Fecha_pago = DateTime.Today;

            var entry = this.iConexion!.Entry<Detalles_Pago>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Detalles_Pago!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}