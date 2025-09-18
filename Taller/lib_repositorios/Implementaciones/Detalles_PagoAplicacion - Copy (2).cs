using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class Detalles_PagoAplicacion : IDetalles_PagoAplicacion
    {
        private IConexion? IConexion = null;

        public Detalles_PagoAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Detalles_Pago? Borrar(Detalles_Pago? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");



            this.IConexion!.Detalles_Pago!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Detalles_Pago? Guardar(Detalles_Pago? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Detalles_Pago!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Detalles_Pago> Listar()
        {
            return this.IConexion!.Detalles_Pago!.Take(20).ToList();
        }

        public List<Detalles_Pago> PorEstudiante(Detalles_Pago? entidad)
        {
            return this.IConexion!.Detalles_Pago!
                .Where(x => x.Metodo_pago!.Contains(entidad!.Metodo_pago!))
                .ToList();
        }

        public Detalles_Pago? Modificar(Detalles_Pago? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Detalles_Pago>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}