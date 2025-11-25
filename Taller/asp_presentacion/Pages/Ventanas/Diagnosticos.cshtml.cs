using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_presentacion.Pages.Ventanas
{
    public class DiagnosticosModel : PageModel
    {
        private IDiagnosticosPresentacion? iPresentacion = null;

        public DiagnosticosModel(IDiagnosticosPresentacion iDiagnosticos)
        {
            try
            {
                this.iPresentacion = iDiagnosticos;
                Filtro = new Diagnosticos();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }

        [BindProperty] public Diagnosticos? Actual { get; set; }
        [BindProperty] public Diagnosticos? Filtro { get; set; }
        [BindProperty] public List<Diagnosticos>? Lista { get; set; }

        public virtual void OnGet()
        {
            OnPostBtRefrescar();

            if (Actual == null)
            {
                Actual = new Diagnosticos();
            }
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Listas;

                Task<List<Diagnosticos>> task = this.iPresentacion!.Listar();

                if (Filtro!.Id_vehiculo > 0)
                {
                    task = this.iPresentacion!.PorVehiculo(Filtro.Id_vehiculo);
                }
                else if (Filtro!.Id_empleado > 0)
                {
                    task = this.iPresentacion!.PorEmpleado(Filtro.Id_empleado);
                }
                else if (Filtro!.Fecha != DateTime.MinValue)
                {
                    DateTime inicio = Filtro.Fecha.Date;
                    DateTime fin = Filtro.Fecha.Date.AddDays(1).AddTicks(-1);
                    task = this.iPresentacion!.PorRangoFechas(inicio, fin);
                }

                task.Wait();
                Lista = task.Result;

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
                Actual = new Diagnosticos();
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
                var actualSeguro = Actual ?? throw new Exception("Error en la carga de datos del formulario.");

                Accion = Enumerables.Ventanas.Editar;

                Task<Diagnosticos?>? task = null;
                if (actualSeguro.Id == 0)
                    task = this.iPresentacion!.Guardar(actualSeguro)!;
                else
                    task = this.iPresentacion!.Modificar(actualSeguro)!;

                task.Wait();
                Actual = task.Result;

                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                Accion = Enumerables.Ventanas.Editar;

                if (Actual == null)
                {
                    Actual = new Diagnosticos();
                }
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

                if (Actual == null || Actual.Id == 0)
                {
                    throw new Exception("Debe especificar un diagnóstico válido para borrar.");
                }

                var task = this.iPresentacion!.Borrar(Actual!);
                task.Wait();

                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);

                if (Actual == null)
                {
                    Actual = new Diagnosticos();
                }
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