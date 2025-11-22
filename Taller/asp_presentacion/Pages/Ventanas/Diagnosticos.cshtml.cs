using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class DiagnosticosModel : PageModel
    {
        private IDiagnosticosPresentacion? iDiagnosticos = null;
        private IVehiculosPresentacion? iVehiculos = null;
        private IEmpleadosPresentacion? iEmpleados = null;

        public DiagnosticosModel(
            IDiagnosticosPresentacion iDiagnosticos,
            IVehiculosPresentacion iVehiculos,
            IEmpleadosPresentacion iEmpleados)
        {
            this.iDiagnosticos = iDiagnosticos;
            this.iVehiculos = iVehiculos;
            this.iEmpleados = iEmpleados;

            Filtro = new Diagnosticos();
        }

        [BindProperty] public Diagnosticos? Actual { get; set; }
        [BindProperty] public Diagnosticos? Filtro { get; set; }
        [BindProperty] public List<Diagnosticos>? Lista { get; set; }

        [BindProperty] public List<Vehiculos>? Vehiculos { get; set; }
        [BindProperty] public List<Empleados>? Empleados { get; set; }

        [BindProperty] public Enumerables.Ventanas Accion { get; set; }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {

                Vehiculos = iVehiculos!.Listar().Result;
                Empleados = iEmpleados!.Listar().Result;

                Accion = Enumerables.Ventanas.Listas;

                var task = iDiagnosticos!.Listar();
                task.Wait();
                Lista = task.Result;

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
            Actual = new Diagnosticos
            {
                Fecha = DateTime.Now
            };

            Vehiculos = iVehiculos!.Listar().Result;
            Empleados = iEmpleados!.Listar().Result;
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();

                Accion = Enumerables.Ventanas.Editar;
                Actual = Lista!.FirstOrDefault(x => x.Id.ToString() == data);

                Vehiculos = iVehiculos!.Listar().Result;
                Empleados = iEmpleados!.Listar().Result;
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

                Task<Diagnosticos>? task = null;

                if (Actual!.Id == 0)
                    task = this.iDiagnosticos!.Guardar(Actual)!;
                else
                    task = this.iDiagnosticos!.Modificar(Actual)!;

                task!.Wait();
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
                var task = iDiagnosticos!.Borrar(Actual!);
                task.Wait();

                OnPostBtRefrescar();
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
