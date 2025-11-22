using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class RepuestosModel : PageModel
    {
        private IRepuestosPresentacion? iPresentacion = null;

        public RepuestosModel(IRepuestosPresentacion iPresentacion)
        {
            this.iPresentacion = iPresentacion;
            Filtro = new Repuestos();
        }


        [BindProperty] public Repuestos? Actual { get; set; }
        [BindProperty] public Repuestos? Filtro { get; set; }
        [BindProperty] public List<Repuestos>? Lista { get; set; }

        [BindProperty] public int StockTotal { get; set; }
        [BindProperty] public Repuestos? MasCaro { get; set; }
        [BindProperty] public Repuestos? MasBarato { get; set; }

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

                Lista = iPresentacion!.Listar().Result;

                if (Filtro != null)
                {

                    if (!string.IsNullOrEmpty(Filtro.Marca))
                        Lista = iPresentacion
                            .PorMarca(Filtro.Marca)
                            .Result;


                    if (Filtro.Id_proveedor != 0)
                        Lista = iPresentacion
                            .PorProveedor(Filtro.Id_proveedor)
                            .Result;


                    if (Filtro.Stock > 0)
                        Lista = iPresentacion
                            .StockBajo(Filtro.Stock)
                            .Result;
                }


                StockTotal = iPresentacion.StockTotal().Result;
                MasCaro = iPresentacion.MasCaro().Result;
                MasBarato = iPresentacion.MasBarato().Result;

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
            Actual = new Repuestos();
        }


        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Accion = Enumerables.Ventanas.Editar;

                Actual = Lista!
                    .FirstOrDefault(x => x.Id.ToString() == data);
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

                Task<Repuestos>? task = null;

                if (Actual!.Id == 0)
                    task = iPresentacion!.Guardar(Actual)!;
                else
                    task = iPresentacion!.Modificar(Actual)!;

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

                Actual = Lista!
                    .FirstOrDefault(x => x.Id.ToString() == data);
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
                var task = iPresentacion!.Borrar(Actual!);
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
