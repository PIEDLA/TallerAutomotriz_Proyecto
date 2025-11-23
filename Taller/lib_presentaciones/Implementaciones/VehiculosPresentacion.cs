using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class VehiculosPresentacion : IVehiculosPresentacion
    {
        private Comunicaciones? comunicaciones = null;

        public async Task<List<Vehiculos>> Listar()
        {
            var lista = new List<Vehiculos>();
            var datos = new Dictionary<string, object>();
            
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Vehiculos/Listar");
            var respuesta = await comunicaciones!.Ejecutar(datos);
            
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Vehiculos>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<List<Vehiculos>> ListarPorCliente(Vehiculos? entidad)
        {
            var lista = new List<Vehiculos>();
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad!;
            
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Vehiculos/ListarPorCliente");
            var respuesta = await comunicaciones!.Ejecutar(datos);
            
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Vehiculos>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<Vehiculos?> Guardar(Vehiculos? entidad)
        {
            if (entidad!.Id != 0)
            {
                throw new Exception("lbFaltaInformacion");
            }
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Vehiculos/Guardar");
            var respuesta = await comunicaciones!.Ejecutar(datos);
            
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Vehiculos>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Vehiculos?> Modificar(Vehiculos? entidad)
        {
            if (entidad!.Id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Vehiculos/Modificar");
            
            var respuesta = await comunicaciones!.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Vehiculos>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Vehiculos?> Borrar(Vehiculos? entidad)
        {
            if (entidad!.Id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Vehiculos/Borrar");
            var respuesta = await comunicaciones!.Ejecutar(datos);
            
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Vehiculos>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
    }
}