using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class Reparacion_HerramientaPresentacion : IReparacion_HerramientaPresentacion
    {
        private Comunicaciones? comunicaciones = null;

        public async Task<List<Reparacion_Herramienta>> Listar()
        {
            var lista = new List<Reparacion_Herramienta>();
            var datos = new Dictionary<string, object>();

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Reparacion_Herramienta/Listar");

            var respuesta = await comunicaciones.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            lista = JsonConversor.ConvertirAObjeto<List<Reparacion_Herramienta>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"])
            );

            return lista;
        }

        public async Task<Reparacion_Herramienta?> Guardar(Reparacion_Herramienta? entidad)
        {
            if (entidad!.Id != 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Reparacion_Herramienta/Guardar");

            var respuesta = await comunicaciones.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            entidad = JsonConversor.ConvertirAObjeto<Reparacion_Herramienta>(
                JsonConversor.ConvertirAString(respuesta["Entidad"])
            );

            return entidad;
        }

        public async Task<Reparacion_Herramienta?> Modificar(Reparacion_Herramienta? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Reparacion_Herramienta/Modificar");

            var respuesta = await comunicaciones.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            entidad = JsonConversor.ConvertirAObjeto<Reparacion_Herramienta>(
                JsonConversor.ConvertirAString(respuesta["Entidad"])
            );

            return entidad;
        }

        public async Task<Reparacion_Herramienta?> Borrar(Reparacion_Herramienta? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Reparacion_Herramienta/Borrar");

            var respuesta = await comunicaciones.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            entidad = JsonConversor.ConvertirAObjeto<Reparacion_Herramienta>(
                JsonConversor.ConvertirAString(respuesta["Entidad"])
            );

            return entidad;
        }

        public async Task<List<Herramientas>> HerramientasPorReparacion(int idReparacion)
        {
            var lista = new List<Herramientas>();
            var datos = new Dictionary<string, object>();
            datos["IdReparacion"] = idReparacion;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Reparacion_Herramienta/HerramientasPorReparacion");

            var respuesta = await comunicaciones.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            lista = JsonConversor.ConvertirAObjeto<List<Herramientas>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"])
            );

            return lista;
        }

        public async Task<List<Reparaciones>> ReparacionesPorHerramienta(int idHerramienta)
        {
            var lista = new List<Reparaciones>();
            var datos = new Dictionary<string, object>();
            datos["IdHerramienta"] = idHerramienta;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Reparacion_Herramienta/ReparacionesPorHerramienta");

            var respuesta = await comunicaciones.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            lista = JsonConversor.ConvertirAObjeto<List<Reparaciones>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"])
            );

            return lista;
        }

        public async Task<int> VecesUsadaHerramienta(int idHerramienta)
        {
            var datos = new Dictionary<string, object>();
            datos["IdHerramienta"] = idHerramienta;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Reparacion_Herramienta/VecesUsadaHerramienta");

            var respuesta = await comunicaciones.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            return int.Parse(respuesta["Cantidad"]!.ToString()!);
        }
    }
}
