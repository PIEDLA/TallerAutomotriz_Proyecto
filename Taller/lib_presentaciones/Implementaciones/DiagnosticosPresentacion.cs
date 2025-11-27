using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class DiagnosticosPresentacion : IDiagnosticosPresentacion
    {
        private Comunicaciones? comunicaciones = null;

        public async Task<List<Diagnosticos>> Listar()
        {
            var lista = new List<Diagnosticos>();
            var datos = new Dictionary<string, object>();
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Diagnosticos/Listar");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            lista = JsonConversor.ConvertirAObjeto<List<Diagnosticos>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"])
            );

            return lista;
        }

        public async Task<List<Diagnosticos>> PorVehiculo(Diagnosticos? entidad)
        {
            var lista = new List<Diagnosticos>();
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad!;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Diagnosticos/PorVehiculo");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Diagnosticos>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<List<Diagnosticos>> PorEmpleado(Diagnosticos? entidad)
        {
            var lista = new List<Diagnosticos>();
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad!;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Diagnosticos/PorEmpleado");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Diagnosticos>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<List<Diagnosticos>> PorRangoFechas(DateTime inicio, DateTime fin)
        {
            var lista = new List<Diagnosticos>();
            var datos = new Dictionary<string, object>
            {
                ["Inicio"] = inicio,
                ["Fin"] = fin
            };
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Diagnosticos/PorRangoFechas");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            lista = JsonConversor.ConvertirAObjeto<List<Diagnosticos>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"])
            );

            return lista;
        }

        public async Task<Diagnosticos?> Guardar(Diagnosticos? entidad)
        {
            if (entidad!.Id != 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Diagnosticos/Guardar");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            return JsonConversor.ConvertirAObjeto<Diagnosticos>(
                JsonConversor.ConvertirAString(respuesta["Entidad"])
            );
        }

        public async Task<Diagnosticos?> Modificar(Diagnosticos? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Diagnosticos/Modificar");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            return JsonConversor.ConvertirAObjeto<Diagnosticos>(
                JsonConversor.ConvertirAString(respuesta["Entidad"])
            );
        }

        public async Task<Diagnosticos?> Borrar(Diagnosticos? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Diagnosticos/Borrar");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            return JsonConversor.ConvertirAObjeto<Diagnosticos>(
                JsonConversor.ConvertirAString(respuesta["Entidad"])
            );
        }

        public async Task<int> ContarPorVehiculo(int idVehiculo)
        {
            var datos = new Dictionary<string, object> { ["IdVehiculo"] = idVehiculo };
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Diagnosticos/ContarPorVehiculo");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            return int.Parse(respuesta["Cantidad"]!.ToString()!);
        }

        public async Task<Diagnosticos?> UltimoDiagnostico()
        {
            var datos = new Dictionary<string, object>();
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Diagnosticos/UltimoDiagnostico");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            return JsonConversor.ConvertirAObjeto<Diagnosticos>(
                JsonConversor.ConvertirAString(respuesta["Entidad"])
            );
        }
    }
}
