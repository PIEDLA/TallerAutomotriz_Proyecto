using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUsuariosPresentacion? iUsuariosPresentacion;

        public IndexModel(IUsuariosPresentacion usuariosPresentacion)
        {
            iUsuariosPresentacion = usuariosPresentacion;
        }

        public bool EstaLogueado = false;

        [BindProperty] public string? Nombre { get; set; }
        [BindProperty] public string? Contraseña { get; set; }

        public void OnGet()
        {
            var variable_session = HttpContext.Session.GetString("Usuario");

            if (!string.IsNullOrEmpty(variable_session))
            {
                EstaLogueado = true;
            }
        }

        public void OnPostBtClean()
        {
            try
            {
                Nombre = string.Empty;
                Contraseña = string.Empty;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public async Task OnPostBtEnter()
        {
            try
            {

                if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Contraseña))
                {
                    OnPostBtClean();
                    return;
                }

                var usuario = new Usuarios
                {
                    Nombre = Nombre,
                    Contraseña = Contraseña
                };

                var respuesta = await iUsuariosPresentacion!.Login(usuario);

                if (respuesta == null)
                {

                    OnPostBtClean();
                    return;
                }

                HttpContext.Session.SetString("Usuario", respuesta.Nombre!);
                HttpContext.Session.SetString("UsuarioId", respuesta.Id.ToString());

                EstaLogueado = true;
                ViewData["Logged"] = true;

                OnPostBtClean();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);

                OnPostBtClean();
            }
        }

        public void OnPostBtClose()
        {
            try
            {
                HttpContext.Session.Clear();
                EstaLogueado = false;

                HttpContext.Response.Redirect("/");
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}
