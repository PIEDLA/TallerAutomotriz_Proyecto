using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class HerramientasPresentacion : IHerramientasPresentacion
    {
        private Comunicaciones? comunicaciones = null;

        public async Task<List<Herramientas>> Listar()
        {
            var lista = new List<Herramientas>();
            var datos = new Dictionary<string, object>();

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Herramientas/Listar");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            lista = JsonConversor.ConvertirAObjeto<List<Herramientas>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"])
            );

            return lista;
        }

        public async Task<Herramientas?> Guardar(Herramientas? entidad)
        {
            if (entidad!.Id != 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Herramientas/Guardar");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            return JsonConversor.ConvertirAObjeto<Herramientas>(
                JsonConversor.ConvertirAString(respuesta["Entidad"])
            );
        }

        public async Task<Herramientas?> Modificar(Herramientas? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Herramientas/Modificar");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            return JsonConversor.ConvertirAObjeto<Herramientas>(
                JsonConversor.ConvertirAString(respuesta["Entidad"])
            );
        }

        public async Task<Herramientas?> Borrar(Herramientas? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Herramientas/Borrar");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            return JsonConversor.ConvertirAObjeto<Herramientas>(
                JsonConversor.ConvertirAString(respuesta["Entidad"])
            );
        }

        public async Task<List<Herramientas>> Disponibles()
        {
            var lista = new List<Herramientas>();
            var datos = new Dictionary<string, object>();
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Herramientas/Disponibles");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            lista = JsonConversor.ConvertirAObjeto<List<Herramientas>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"])
            );

            return lista;
        }

        public async Task<List<Herramientas>> EnMantenimiento()
        {
            var lista = new List<Herramientas>();
            var datos = new Dictionary<string, object>();
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Herramientas/EnMantenimiento");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            lista = JsonConversor.ConvertirAObjeto<List<Herramientas>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"])
            );

            return lista;
        }

        public async Task<List<Herramientas>> PorTipo(string tipo)
        {
            var lista = new List<Herramientas>();
            var filtroEntidad = new Herramientas { Tipo = tipo };
            var datos = new Dictionary<string, object> { ["Entidad"] = filtroEntidad };
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Herramientas/PorTipo");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            lista = JsonConversor.ConvertirAObjeto<List<Herramientas>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"])
            );

            return lista;
        }

        public async Task<int> TotalHerramientas()
        {
            var datos = new Dictionary<string, object>();
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Herramientas/TotalHerramientas");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            return int.Parse(respuesta["Cantidad"]!.ToString()!);
        }

        public async Task<int> TotalDisponibles()
        {
            var datos = new Dictionary<string, object>();
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Herramientas/TotalDisponibles");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            return int.Parse(respuesta["Cantidad"]!.ToString()!);
        }
    }
}