using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class ReparacionesPresentacion : IReparacionesPresentacion
    {
        private Comunicaciones? comunicaciones = null;

        public async Task<List<Reparaciones>> Listar()
        {
            var lista = new List<Reparaciones>();
            var datos = new Dictionary<string, object>();

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Reparaciones/Listar");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            lista = JsonConversor.ConvertirAObjeto<List<Reparaciones>>(
                        JsonConversor.ConvertirAString(respuesta["Entidades"]));

            return lista;
        }

        public async Task<Reparaciones?> Guardar(Reparaciones? entidad)
        {
            if (entidad!.Id != 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Reparaciones/Guardar");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<Reparaciones>(
                            JsonConversor.ConvertirAString(respuesta["Entidad"]));

            return entidad;
        }

        public async Task<Reparaciones?> Modificar(Reparaciones? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Reparaciones/Modificar");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<Reparaciones>(
                            JsonConversor.ConvertirAString(respuesta["Entidad"]));

            return entidad;
        }

        public async Task<Reparaciones?> Borrar(Reparaciones? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Reparaciones/Borrar");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<Reparaciones>(
                            JsonConversor.ConvertirAString(respuesta["Entidad"]));

            return entidad;
        }


        public async Task<List<Reparaciones>> PorDiagnostico(int idDiagnostico)
        {
            var lista = new List<Reparaciones>();
            var datos = new Dictionary<string, object>();
            datos["Id"] = idDiagnostico;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Reparaciones/PorDiagnostico");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            lista = JsonConversor.ConvertirAObjeto<List<Reparaciones>>(
                        JsonConversor.ConvertirAString(respuesta["Entidades"]));

            return lista;
        }

        public async Task<List<Reparaciones>> Costosas(decimal minimo)
        {
            var lista = new List<Reparaciones>();
            var datos = new Dictionary<string, object>();
            datos["Minimo"] = minimo;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Reparaciones/Costosas");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            lista = JsonConversor.ConvertirAObjeto<List<Reparaciones>>(
                        JsonConversor.ConvertirAString(respuesta["Entidades"]));

            return lista;
        }

        public async Task<List<Reparaciones>> EntreFechas(DateTime inicio, DateTime fin)
        {
            var lista = new List<Reparaciones>();
            var datos = new Dictionary<string, object>();
            datos["Inicio"] = inicio;
            datos["Fin"] = fin;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Reparaciones/EntreFechas");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            lista = JsonConversor.ConvertirAObjeto<List<Reparaciones>>(
                        JsonConversor.ConvertirAString(respuesta["Entidades"]));

            return lista;
        }

        public async Task<Reparaciones?> UltimaReparacion()
        {
            var datos = new Dictionary<string, object>();

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Reparaciones/UltimaReparacion");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Reparaciones>(
                        JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<decimal> TotalEstimado()
        {
            var datos = new Dictionary<string, object>();

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Reparaciones/TotalEstimado");

            var respuesta = await comunicaciones.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return Convert.ToDecimal(respuesta["Valor"]);
        }
    }
}
