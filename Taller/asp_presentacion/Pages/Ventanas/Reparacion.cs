using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class ReparacionesModel : PageModel
    {
        private IReparacionesPresentacion? iReparaciones = null;

        public ReparacionesModel(IReparacionesPresentacion iReparaciones)
        {
            this.iReparaciones = iReparaciones;
            Filtro = new Reparaciones();
        }

        [BindProperty] public Reparaciones? Actual { get; set; }
        [BindProperty] public Reparaciones? Filtro { get; set; }
        [BindProperty] public List<Reparaciones>? Lista { get; set; }

        [BindProperty] public DateTime FechaInicioFiltro { get; set; }
        [BindProperty] public DateTime FechaFinFiltro { get; set; }

        [BindProperty] public Reparaciones? UltimaReparacion { get; set; }
        [BindProperty] public decimal TotalEstimado { get; set; }

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

                Lista = iReparaciones!.Listar().Result;


                if (Filtro != null)
                {

                    if (Filtro.Id_diagnostico != 0)
                        Lista = iReparaciones
                            .PorDiagnostico(Filtro.Id_diagnostico)
                            .Result;


                    if (Filtro.Costo_estimado > 0)
                        Lista = iReparaciones
                            .Costosas(Filtro.Costo_estimado)
                            .Result;


                    if (FechaInicioFiltro != DateTime.MinValue &&
                        FechaFinFiltro != DateTime.MinValue)
                    {
                        Lista = iReparaciones
                            .EntreFechas(FechaInicioFiltro, FechaFinFiltro)
                            .Result;
                    }
                }


                UltimaReparacion = iReparaciones.UltimaReparacion().Result;
                TotalEstimado = iReparaciones.TotalEstimado().Result;

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
            Actual = new Reparaciones
            {
                Fecha_inicio = DateTime.Now
            };
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

                Task<Reparaciones>? task = null;

                if (Actual!.Id == 0)
                    task = iReparaciones!.Guardar(Actual)!;
                else
                    task = iReparaciones!.Modificar(Actual)!;

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
                var task = iReparaciones!.Borrar(Actual!);
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
