using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_repositorios.Interfaces;

namespace lib_repositorios.Implementaciones
{
    public class TokenAplicacion
    {
        private IConexion? IConexion = null;
        // El profe dice mejore las llaves
        private string llave = TokenGenerator.GenerateToken();

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
            return this.llave == datos["Llave"].ToString();
        }
    }
}