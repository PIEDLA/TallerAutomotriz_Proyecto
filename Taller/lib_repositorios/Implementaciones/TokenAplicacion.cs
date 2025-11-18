using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_repositorios.Interfaces;

namespace lib_repositorios.Implementaciones
{
    public class TokenAplicacion
    {
        private IConexion? IConexion = null;
        // El profe dice mejore las llaves
        private static string llave = "";

        public TokenAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public string Llave(Usuarios? entidad)
        {
            llave = TokenGenerator.GenerateToken();
            var usuario = this.IConexion!.Usuarios!
                .FirstOrDefault(x => x.Nombre == entidad!.Nombre && 
                                x.Contraseña == entidad.Contraseña);
            if (usuario == null)
                return string.Empty;
            return llave;
        }

        public bool Validar(Dictionary<string, object> datos)
        {
            if (!datos.ContainsKey("Llave"))
                return false;
            return llave == datos["Llave"].ToString();
        }
    }
}