using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class Detalles_RepuestoPrueba2
    {
        private readonly IConexion? iConexion;
        private List<Detalles_Repuesto>? lista;
        private Detalles_Repuesto? entidad;

        public Detalles_RepuestoPrueba2()
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
            this.lista = this.iConexion!.Detalles_Repuesto!
            .Include(x => x._Factura)
            .Include(x => x._Repuesto)
            .ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Detalles_Repuesto()!;
            this.iConexion!.Detalles_Repuesto!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Factura = 2;

            var entry = this.iConexion!.Entry<Detalles_Repuesto>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Detalles_Repuesto!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}