using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class FacturasAplicacion : IFacturasAplicacion
    {
        private IConexion? IConexion = null;

        public FacturasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Facturas? Borrar(Facturas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Operaciones
            entidad._Cliente = null;
            entidad._Reparacion = null;

            var facturaExistente = this.IConexion!.Facturas!.FirstOrDefault(x => x.Id == entidad.Id);

            if (facturaExistente == null)
                throw new Exception("La factura que intenta eliminar no existe");

            this.IConexion!.Facturas!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Facturas? Guardar(Facturas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Operaciones
            entidad._Cliente = null;
            entidad._Reparacion = null;

            if (entidad.Fecha_emision > DateTime.Now)
                throw new Exception("La fecha de emisión no puede ser futura");

            ValidarFacturasVencidasCliente(entidad.Id_cliente);

            this.IConexion!.Facturas!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private void ValidarFacturasVencidasCliente(int clienteId)
        {
            var fechaLimite = DateTime.Now.AddDays(-30); // 30 días de vencimiento

            var facturas_vencidas = this.IConexion!.Facturas!.Where(x => x.Id_cliente == clienteId && x.Fecha_emision < fechaLimite)
                                    .Count(x => !this.IConexion.Pagos!.Any(p => p.Id_factura == x.Id));

            if (facturas_vencidas > 3)
                throw new Exception("El cliente tiene más de 3 facturas vencidas pendientes de pago");
        }

        public List<Facturas> Listar()
        {
            return this.IConexion!.Facturas!.ToList();
        }

        public List<Facturas> ListarPorCliente(int clienteId)
        {
            if (clienteId <= 0)
                throw new Exception("Debe especificar un cliente válido");

            return this.IConexion!.Facturas!.Include(x => x._Cliente).Include(x => x._Reparacion)
                                            .Where(x => x.Id_cliente == clienteId).ToList();
        }

        public Facturas? Modificar(Facturas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Operaciones
            entidad._Cliente = null;
            entidad._Reparacion = null;

            var facturaExistente = this.IConexion!.Facturas!.FirstOrDefault(x => x.Id == entidad.Id);

            if (facturaExistente == null)
                throw new Exception("La factura que intenta modificar no existe");

            var entry = this.IConexion!.Entry<Facturas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
