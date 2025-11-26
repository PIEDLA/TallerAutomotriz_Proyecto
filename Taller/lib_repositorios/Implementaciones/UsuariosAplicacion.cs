using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class UsuariosAplicacion : IUsuariosAplicacion
    {
        private IConexion? IConexion = null;

        public UsuariosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Usuarios? Borrar(Usuarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            entidad._Funcion = null;

            this.IConexion!.Usuarios!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Usuarios? Guardar(Usuarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Operaciones

            var vehiculoExistente = this.IConexion!.Usuarios!.FirstOrDefault(x => x.Nombre!.ToUpper() == entidad.Nombre!.ToUpper());

            if (vehiculoExistente != null)
                throw new Exception("Ya existe este usuario registrado, elige otro diferente");

            if (string.IsNullOrWhiteSpace(entidad.Nombre))
                throw new Exception("La nombre es obligatoria.");

            if (string.IsNullOrWhiteSpace(entidad.Contraseña))
                throw new Exception("La contraseña es obligatoria.");

            if ((entidad.Funcion) <= 1)
                throw new Exception("Su función es obligatorio.");

            var funcion = this.IConexion!.Funciones!.Find(entidad!.Funcion);
            funcion!.Usuarios!.Add(entidad);

            this.IConexion!.Usuarios!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Usuarios> Listar()
        {
            return this.IConexion!.Usuarios!.ToList();
        }

        public Usuarios? Modificar(Usuarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");


            if (string.IsNullOrWhiteSpace(entidad.Nombre))
                throw new Exception("La nombre es obligatoria.");

            if (string.IsNullOrWhiteSpace(entidad.Contraseña))
                throw new Exception("La contraseña es obligatoria.");

            if ((entidad.Funcion) <= 1)
                throw new Exception("Su función es obligatorio.");

            entidad._Funcion = null;

            var entry = this.IConexion!.Entry<Usuarios>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Usuarios? Login(string nombre, string contraseña)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(contraseña))
                throw new Exception("lbFaltaInformacion");

            var usuario = this.IConexion!.Usuarios!
                .FirstOrDefault(x =>
                    x.Nombre!.ToUpper() == nombre.ToUpper() &&
                    x.Contraseña == contraseña
                );

            if (usuario == null)
                throw new Exception("lbCredencialesInvalidas");

            return usuario;
        }

        public Usuarios Registrar(Usuarios entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (string.IsNullOrWhiteSpace(entidad.Nombre))
                throw new Exception("lbFaltaInformacion");

            if (string.IsNullOrWhiteSpace(entidad.Contraseña))
                throw new Exception("lbFaltaInformacion");

            var usuarioExistente = this.IConexion!.Usuarios!
                .FirstOrDefault(x => x.Nombre!.ToUpper() == entidad.Nombre!.ToUpper());

            if (usuarioExistente != null)
                throw new Exception("lbYaExiste");

            if (entidad.Funcion <= 0)
                throw new Exception("lbFaltaInformacion");

            entidad._Funcion = null;

            this.IConexion!.Usuarios!.Add(entidad);
            this.IConexion.SaveChanges();

            return entidad;
        }


    }
}