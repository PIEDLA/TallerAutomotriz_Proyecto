using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class PagosAplicacion : IPagosAplicacion
    {
        private IConexion? IConexion = null;

        public PagosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public List<Pagos> Listar()
        {
            return this.IConexion!.Pagos!
                .Include(p => p._Factura)
                .Take(20)
                .ToList();
        }

        public Pagos? Guardar(Pagos? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información del pago");

            if (entidad.Id != 0)
                throw new Exception("El pago ya existe");

            if (entidad.Monto_total <= 0)
                throw new Exception("El monto debe ser mayor a 0");

            if (entidad.Fecha_pago > DateTime.Now)
                throw new Exception("La fecha no puede ser futura");

            var factura = this.IConexion!.Facturas!.FirstOrDefault(f => f.Id == entidad.Id_factura);
            if (factura == null)
                throw new Exception("La factura no existe");

            entidad.Estado = entidad.Monto_total >= factura.Total ? "Pagado" : "Pendiente";

            entidad._Factura = null;

            this.IConexion.Pagos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Pagos? Modificar(Pagos? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información del pago");

            if (entidad.Id == 0)
                throw new Exception("El pago no existe");

            if (entidad.Monto_total <= 0)
                throw new Exception("El monto debe ser mayor a 0");

            if (entidad.Fecha_pago > DateTime.Now)
                throw new Exception("La fecha no puede ser futura");

            var factura = this.IConexion!.Facturas!.FirstOrDefault(f => f.Id == entidad.Id_factura);
            if (factura == null)
                throw new Exception("La factura no existe");

            entidad.Estado = entidad.Monto_total >= factura.Total ? "Pagado" : "Pendiente";
            entidad._Factura = null;

            var entry = this.IConexion.Entry<Pagos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Pagos? Borrar(Pagos? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información del pago");

            if (entidad.Id == 0)
                throw new Exception("El pago no existe");

            entidad._Factura = null;

            this.IConexion!.Pagos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Pagos> PorFactura(int idFactura)
        {
            return this.IConexion!.Pagos!
                .Where(p => p.Id_factura == idFactura)
                .ToList();
        }

        public List<Pagos> PorEstado(string estado)
        {
            return this.IConexion!.Pagos!
                .Where(p => p.Estado == estado)
                .ToList();
        }

        public List<Pagos> PorFecha(DateTime fecha)
        {
            return this.IConexion!.Pagos!
                .Where(p => p.Fecha_pago.Date == fecha.Date)
                .ToList();
        }

        public decimal TotalPagos()
        {
            return this.IConexion!.Pagos!.Sum(p => p.Monto_total);
        }

        public Pagos? UltimoPago()
        {
            return this.IConexion!.Pagos!
                .OrderByDescending(p => p.Fecha_pago)
                .FirstOrDefault();
        }
    }
}
