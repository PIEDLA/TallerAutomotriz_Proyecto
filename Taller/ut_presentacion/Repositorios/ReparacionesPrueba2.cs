using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class ReparacionesPrueba2
    {
        private readonly IConexion? iConexion;
        private List<Reparaciones>? lista;
        private Reparaciones? entidad;

        public ReparacionesPrueba2()
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
            this.lista = this.iConexion!.Reparaciones!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = new Reparaciones
            {
                Id_diagnostico = 2,
                Descripcion_trabajo = "Alineación y balanceo",
                Costo_estimado = 300.00m,
                Fecha_inicio = DateTime.Now
            };
            this.iConexion!.Reparaciones!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Costo_estimado = 350.00m;
            var entry = this.iConexion!.Entry<Reparaciones>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Reparaciones!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}
