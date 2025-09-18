using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class ReparacionesPrueba
    {
        private readonly IConexion? iConexion;
        private List<Reparaciones>? lista;
        private Reparaciones? entidad;

        public ReparacionesPrueba()
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
            this.lista = this.iConexion!.Reparaciones!
            .Include(x => x._Diagnostico)
            .ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Reparaciones()!;
            this.iConexion!.Reparaciones!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Descripcion_trabajo = "Trabajo actualizado desde prueba";
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
