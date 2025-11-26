using lib_dominio.Entidades;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUsuariosPresentacion? UsuariosPresentacion;

        public RegisterModel(IUsuariosPresentacion usuariosPresentacion)
        {
            UsuariosPresentacion = usuariosPresentacion;
        }

        public bool Registrado { get; set; } = false;

        [BindProperty] public string? Nombre { get; set; }
        [BindProperty] public string? Contraseña { get; set; }
        [BindProperty] public int Funcion { get; set; }

        public void OnGet()
        {
        }

        public void OnPostBtClean()
        {
            Nombre = "";
            Contraseña = "";
            Funcion = 0;
        }

        public async Task<IActionResult> OnPostBtRegister()
        {
            try
            {
                var usuario = new Usuarios
                {
                    Nombre = this.Nombre,
                    Contraseña = this.Contraseña,
                    Funcion = this.Funcion
                };

                var respuesta = await UsuariosPresentacion!.Registrar(usuario);

                if (respuesta != null)
                {
                    Registrado = true;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Page();
        }
    }
}
