using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class Reserva_ServicioPrueba
    {
        private readonly IConexion? iConexion;
        private List<Reserva_Servicio>? lista;
        private Reserva_Servicio? entidad;

        public Reserva_ServicioPrueba()
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
            this.lista = this.iConexion!.Reserva_Servicio!
            .Include(x => x._Reserva)
            .Include(x => x._Servicio)
            .ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Reserva_Servicio()!;
            this.iConexion!.Reserva_Servicio!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Servicio = 2;

            var entry = this.iConexion!.Entry<Reserva_Servicio>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Reserva_Servicio!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}