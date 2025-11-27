using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace asp_presentacion.Pages.Ventanas
{
    public class VehiculosModel : PageModel
    {
        private IVehiculosPresentacion? iPresentacion = null;
        private IDiagnosticosPresentacion? iDiagnosticosPresentacion = null;

        public VehiculosModel(IVehiculosPresentacion iPresentacion, IDiagnosticosPresentacion iDiagnosticosPresentacion)
        {
            try
            {
                this.iPresentacion = iPresentacion;
                this.iDiagnosticosPresentacion = iDiagnosticosPresentacion;
                Filtro = new Vehiculos();
                FiltroD = new Diagnosticos();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }


        [BindProperty] public Vehiculos? Actual { get; set; }
        [BindProperty] public Vehiculos? Filtro { get; set; }
        [BindProperty] public List<Vehiculos>? Lista { get; set; }
        [BindProperty] public Diagnosticos? ActualD { get; set; }
        [BindProperty] public Diagnosticos? FiltroD { get; set; }
        [BindProperty] public List<Diagnosticos>? ListaD { get; set; }

        public virtual void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                //var variable_session = HttpContext.Session.GetString("Usuario");
                //if (String.IsNullOrEmpty(variable_session))
                //{
                //    HttpContext.Response.Redirect("/");
                //    return;
                //}

                Filtro!.Placa = Filtro!.Placa ?? "";

                Accion = Enumerables.Ventanas.Listas;
                var task = this.iPresentacion!.PorPlaca(Filtro!);
                task.Wait();
                Lista = task.Result;
                Actual = null;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtNuevo()
        {
            try
            {
                Accion = Enumerables.Ventanas.Editar;
                Actual = new Vehiculos();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtModificar(string data)
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

        public virtual void OnPostBtGuardar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Editar;

                Task<Vehiculos>? task = null;
                if (Actual!.Id == 0)
                    task = this.iPresentacion!.Guardar(Actual!)!;
                else
                    task = this.iPresentacion!.Modificar(Actual!)!;
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

        public virtual void OnPostBtBorrarVal(string data)
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

        public virtual void OnPostBtBorrar()
        {
            try
            {
                var task = this.iPresentacion!.Borrar(Actual!);
                Actual = task.Result;
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

        public virtual void OnPostBtVerDiag(int id)
        {
            try
            {
                Accion = Enumerables.Ventanas.Sublistas;

                FiltroD!.Id_vehiculo = id;
                var task = this.iDiagnosticosPresentacion!.PorVehiculo(FiltroD);
                task.Wait();
                ListaD = task.Result;

                Actual = ListaD!.First()._Vehiculo;

                ActualD = null;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

    }
}