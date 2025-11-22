using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class PagosModel : PageModel
    {
        private IPagosPresentacion? iPagos = null;

        public PagosModel(IPagosPresentacion iPagos)
        {
            this.iPagos = iPagos;
            Filtro = new Pagos();
        }

        [BindProperty] public Pagos? Actual { get; set; }
        [BindProperty] public Pagos? Filtro { get; set; }
        [BindProperty] public List<Pagos>? Lista { get; set; }

        [BindProperty] public decimal TotalPagos { get; set; }
        [BindProperty] public Pagos? UltimoPago { get; set; }

        [BindProperty] public Enumerables.Ventanas Accion { get; set; }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Listas;
                Lista = iPagos!.Listar().Result;

                if (Filtro != null)
                {
                    if (Filtro.Fecha_pago != DateTime.MinValue)
                        Lista = iPagos.PorFecha(Filtro.Fecha_pago).Result;

                    if (Filtro.Id_factura != 0)
                        Lista = iPagos.PorFactura(Filtro.Id_factura).Result;

                    if (!string.IsNullOrEmpty(Filtro.Estado))
                        Lista = iPagos.PorEstado(Filtro.Estado).Result;
                }


                TotalPagos = iPagos.TotalPagos().Result;
                UltimoPago = iPagos.UltimoPago().Result;

                Actual = null;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }


        public void OnPostBtNuevo()
        {
            Accion = Enumerables.Ventanas.Editar;
            Actual = new Pagos { Fecha_pago = DateTime.Now };
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Accion = Enumerables.Ventanas.Editar;
                Actual = Lista!.FirstOrDefault(x => x.Id.ToString() == data);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Editar;

                Task<Pagos>? task = null;
                if (Actual!.Id == 0)
                    task = iPagos!.Guardar(Actual)!;
                else
                    task = iPagos!.Modificar(Actual)!;

                task.Wait();
                Actual = task.Result;

                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtBorrarVal(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Accion = Enumerables.Ventanas.Borrar;
                Actual = Lista!.FirstOrDefault(x => x.Id.ToString() == data);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                var task = iPagos!.Borrar(Actual!);
                task.Wait();
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtCancelar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtCerrar()
        {
            try
            {
                if (Accion == Enumerables.Ventanas.Listas)
                    OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}
