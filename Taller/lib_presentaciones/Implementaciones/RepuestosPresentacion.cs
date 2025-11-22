using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class RepuestosPresentacion : IRepuestosPresentacion
    {
        private Comunicaciones? comunicaciones = null;

        public async Task<List<Repuestos>> Listar()
        {
            var lista = new List<Repuestos>();
            var datos = new Dictionary<string, object>();

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Repuestos/Listar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            lista = JsonConversor.ConvertirAObjeto<List<Repuestos>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));

            return lista;
        }

        public async Task<Repuestos?> Guardar(Repuestos? entidad)
        {
            if (entidad!.Id != 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Repuestos/Guardar");

            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<Repuestos>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));

            return entidad;
        }

        public async Task<Repuestos?> Modificar(Repuestos? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Repuestos/Modificar");

            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<Repuestos>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));

            return entidad;
        }

        public async Task<Repuestos?> Borrar(Repuestos? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Repuestos/Borrar");

            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<Repuestos>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));

            return entidad;
        }


        public async Task<List<Repuestos>> StockBajo(int limite = 5)
        {
            var datos = new Dictionary<string, object>();
            datos["Limite"] = limite;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Repuestos/StockBajo");

            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Repuestos>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<List<Repuestos>> PorMarca(string marca)
        {
            var datos = new Dictionary<string, object>();
            datos["Marca"] = marca;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Repuestos/PorMarca");

            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Repuestos>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<List<Repuestos>> PorProveedor(int idProveedor)
        {
            var datos = new Dictionary<string, object>();
            datos["IdProveedor"] = idProveedor;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Repuestos/PorProveedor");

            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Repuestos>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<Repuestos?> MasCaro()
        {
            comunicaciones = new Comunicaciones();
            var datos = comunicaciones.ConstruirUrl(new(), "Repuestos/MasCaro");

            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Repuestos>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Repuestos?> MasBarato()
        {
            comunicaciones = new Comunicaciones();
            var datos = comunicaciones.ConstruirUrl(new(), "Repuestos/MasBarato");

            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Repuestos>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<int> StockTotal()
        {
            comunicaciones = new Comunicaciones();
            var datos = comunicaciones.ConstruirUrl(new(), "Repuestos/StockTotal");

            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return int.Parse(respuesta["Valor"].ToString()!);
        }
    }
}
