using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class Reparacion_HerramientaModel : PageModel
    {
        private IReparacion_HerramientaPresentacion? iReparacion_Herramienta = null;

        public Reparacion_HerramientaModel(IReparacion_HerramientaPresentacion iReparacion_Herramienta)
        {
            this.iReparacion_Herramienta = iReparacion_Herramienta;
            Filtro = new Reparacion_Herramienta();
        }

        [BindProperty] public Reparacion_Herramienta? Actual { get; set; }
        [BindProperty] public Reparacion_Herramienta? Filtro { get; set; }
        [BindProperty] public List<Reparacion_Herramienta>? Lista { get; set; }

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
                Lista = iReparacion_Herramienta!.Listar().Result;

                if (Filtro!.Id_reparacion != 0)
                    Lista = Lista.Where(x => x.Id_reparacion == Filtro.Id_reparacion).ToList();

                if (Filtro.Id_herramienta != 0)
                    Lista = Lista.Where(x => x.Id_herramienta == Filtro.Id_herramienta).ToList();

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
            Actual = new Reparacion_Herramienta();
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

                Task<Reparacion_Herramienta>? task = null;
                if (Actual!.Id == 0)
                    task = iReparacion_Herramienta!.Guardar(Actual)!;
                else
                    task = iReparacion_Herramienta!.Modificar(Actual)!;

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
                var task = iReparacion_Herramienta!.Borrar(Actual!);
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
