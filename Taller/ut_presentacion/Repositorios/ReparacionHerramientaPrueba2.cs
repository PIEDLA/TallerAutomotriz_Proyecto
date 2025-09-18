using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class ReparacionHerramientaPrueba2
    {
        private readonly IConexion? iConexion;
        private List<Reparacion_Herramienta>? lista;
        private Reparacion_Herramienta? entidad;

        public ReparacionHerramientaPrueba2()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }

        public bool Listar()
        {
            this.lista = this.iConexion!.Reparacion_Herramienta!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = new Reparacion_Herramienta
            {
                Id_reparacion = 2, 
                Id_herramienta = 2  
            };
            this.iConexion!.Reparacion_Herramienta!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Reparacion_Herramienta!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}
