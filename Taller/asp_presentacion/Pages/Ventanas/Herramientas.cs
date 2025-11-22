using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class HerramientasModel : PageModel
    {
        private IHerramientasPresentacion? iHerramientas = null;

        public HerramientasModel(IHerramientasPresentacion iHerramientas)
        {
            this.iHerramientas = iHerramientas;
            Filtro = new Herramientas();
        }

        [BindProperty] public Herramientas? Actual { get; set; }
        [BindProperty] public Herramientas? Filtro { get; set; }
        [BindProperty] public List<Herramientas>? Lista { get; set; }

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
                Lista = iHerramientas!.Listar().Result;

                if (!string.IsNullOrEmpty(Filtro!.Tipo))
                    Lista = Lista.Where(x => x.Tipo == Filtro.Tipo).ToList();

                if (!string.IsNullOrEmpty(Filtro.Estado))
                    Lista = Lista.Where(x => x.Estado == Filtro.Estado).ToList();

                if (!string.IsNullOrEmpty(Filtro.Ubicacion))
                    Lista = Lista.Where(x => x.Ubicacion!.Contains(Filtro.Ubicacion)).ToList();

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
            Actual = new Herramientas();
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

                Task<Herramientas>? task = null;
                if (Actual!.Id == 0)
                    task = iHerramientas!.Guardar(Actual)!;
                else
                    task = iHerramientas!.Modificar(Actual)!;

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
                var task = iHerramientas!.Borrar(Actual!);
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
