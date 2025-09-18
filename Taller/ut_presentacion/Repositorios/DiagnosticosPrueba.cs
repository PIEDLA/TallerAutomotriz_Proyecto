using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class DiagnosticosPrueba
    {
        private readonly IConexion? iConexion;
        private List<Diagnosticos>? lista;
        private Diagnosticos? entidad;

        public DiagnosticosPrueba()
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
            this.lista = this.iConexion!.Diagnosticos!
            .Include(x => x._Empleado)
            .Include(x => x._Vehiculo)
            .ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = new Diagnosticos
            {
                Id_vehiculo = 1,
                Id_empleado = 1,
                Descripcion = "Prueba inicial",
                Fecha = DateTime.Now
            };
            this.iConexion!.Diagnosticos!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Descripcion = "Descripcion modificada";
            var entry = this.iConexion!.Entry<Diagnosticos>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Diagnosticos!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}
