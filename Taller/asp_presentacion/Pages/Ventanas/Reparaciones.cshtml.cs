using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;
using System;

namespace asp_presentacion.Pages.Ventanas
{
    public class ReparacionesModel : PageModel
    {
        private IReparacionesPresentacion? iPresentacion = null;

        public ReparacionesModel(IReparacionesPresentacion iReparaciones)
        {
            try
            {
                this.iPresentacion = iReparaciones;
                Filtro = new Reparaciones();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }

        [BindProperty] public Reparaciones? Actual { get; set; }
        [BindProperty] public Reparaciones? Filtro { get; set; }
        [BindProperty] public List<Reparaciones>? Lista { get; set; }


        [BindProperty] public DateTime? Fecha_inicio_filtro { get; set; }
        [BindProperty] public DateTime? Fecha_fin_filtro { get; set; }


        public virtual void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Listas;


                Task<List<Reparaciones>> task = this.iPresentacion!.Listar();
                task.Wait();
                Lista = task.Result;


                if (Filtro!.Id_diagnostico > 0)
                {
                    Lista = Lista!.Where(x => x.Id_diagnostico == Filtro.Id_diagnostico).ToList();
                }


                if (Filtro!.Costo_estimado > 0)
                {
                    Lista = Lista!.Where(x => x.Costo_estimado >= Filtro.Costo_estimado).ToList();
                }

                if (Fecha_inicio_filtro.HasValue && Fecha_fin_filtro.HasValue)
                {
                    DateTime inicio = Fecha_inicio_filtro.Value.Date;
                    DateTime fin = Fecha_fin_filtro.Value.Date;

                    DateTime finInclusivo = fin.AddDays(1).AddSeconds(-1);

                    Lista = Lista!.Where(r => r.Fecha_inicio >= inicio && r.Fecha_inicio <= finInclusivo).ToList();
                }


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
                Actual = new Reparaciones();
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

                Task<Reparaciones?>? task = null;
                if (Actual!.Id == 0)
                    task = this.iPresentacion!.Guardar(Actual!);
                else
                    task = this.iPresentacion!.Modificar(Actual!);

                task.Wait();
                Actual = task.Result;

                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);

                Accion = Enumerables.Ventanas.Editar;
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
                task.Wait();
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
    }
}