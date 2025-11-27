using asp_servicios.Nucleo;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DiagnosticosController : ControllerBase
    {
        private IDiagnosticosAplicacion? iAplicacion = null;
        private TokenAplicacion? iAplicacionToken = null;

        public DiagnosticosController(IDiagnosticosAplicacion? iAplicacion, TokenAplicacion iAplicacionToken)
        {
            this.iAplicacion = iAplicacion;
            this.iAplicacionToken = iAplicacionToken;
        }

        private Dictionary<string, object> ObtenerDatos()
        {
            var datos = new StreamReader(Request.Body).ReadToEnd();
            if (string.IsNullOrEmpty(datos))
                datos = "{}";
            return JsonConversor.ConvertirAObjeto(datos);
        }


        [HttpPost]
        public string Listar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!iAplicacionToken!.Validar(datos))
                {
                    respuesta["Error"] = "No autenticado.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                respuesta["Entidades"] = iAplicacion.Listar();
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now;

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message;
                respuesta["Respuesta"] = "Error";
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        [HttpPost]
        public string Guardar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!iAplicacionToken!.Validar(datos))
                {
                    respuesta["Error"] = "No autenticado.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                if (!datos.ContainsKey("Entidad"))
                {
                    respuesta["Error"] = "Falta la entidad a guardar.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                var entidad = JsonConversor.ConvertirAObjeto<Diagnosticos>(
                    JsonConversor.ConvertirAString(datos["Entidad"])
                );

                if (entidad == null || entidad.Id_vehiculo <= 0 || entidad.Id_empleado <= 0)
                {
                    respuesta["Error"] = "Debe especificar un vehículo y un empleado válidos.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                entidad = iAplicacion.Guardar(entidad);

                respuesta["Entidad"] = entidad!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now;

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message;
                respuesta["Respuesta"] = "Error";
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        [HttpPost]
        public string Modificar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!iAplicacionToken!.Validar(datos))
                {
                    respuesta["Error"] = "No autenticado.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                var entidad = JsonConversor.ConvertirAObjeto<Diagnosticos>(
                    JsonConversor.ConvertirAString(datos["Entidad"])
                );

                if (entidad == null || entidad.Id <= 0)
                {
                    respuesta["Error"] = "Debe especificar un diagnóstico válido para modificar.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                entidad = iAplicacion.Modificar(entidad);

                respuesta["Entidad"] = entidad!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now;

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message;
                respuesta["Respuesta"] = "Error";
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        [HttpPost]
        public string Borrar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!iAplicacionToken!.Validar(datos))
                {
                    respuesta["Error"] = "No autenticado.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                var entidad = JsonConversor.ConvertirAObjeto<Diagnosticos>(
                    JsonConversor.ConvertirAString(datos["Entidad"])
                );

                if (entidad == null || entidad.Id <= 0)
                {
                    respuesta["Error"] = "Debe especificar un diagnóstico válido para borrar.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                entidad = iAplicacion.Borrar(entidad);

                respuesta["Entidad"] = entidad!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now;

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message;
                respuesta["Respuesta"] = "Error";
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        [HttpPost]
        public string PorVehiculo()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!iAplicacionToken!.Validar(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }
                var entidad = JsonConversor.ConvertirAObjeto<Diagnosticos>(
                JsonConversor.ConvertirAString(datos["Entidad"]));
                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));

                respuesta["Entidades"] = this.iAplicacion!.PorVehiculo(entidad!);
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                respuesta["Respuesta"] = "Error";
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        [HttpPost]
        public string PorEmpleado()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!iAplicacionToken!.Validar(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }
                var entidad = JsonConversor.ConvertirAObjeto<Diagnosticos>(
                JsonConversor.ConvertirAString(datos["Entidad"]));
                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));

                respuesta["Entidades"] = this.iAplicacion!.PorEmpleado(entidad!);
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                respuesta["Respuesta"] = "Error";
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        [HttpPost]
        public string PorRangoFechas()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!iAplicacionToken!.Validar(datos))
                {
                    respuesta["Error"] = "No autenticado.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                if (!datos.ContainsKey("FechaInicio") || !datos.ContainsKey("FechaFin"))
                {
                    respuesta["Error"] = "Debe especificar las fechas de inicio y fin.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                DateTime inicio = DateTime.Parse(datos["FechaInicio"].ToString()!);
                DateTime fin = DateTime.Parse(datos["FechaFin"].ToString()!);
                iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));

                var lista = iAplicacion.PorRangoFechas(inicio, fin);
                if (lista == null || lista.Count == 0)
                    respuesta["Mensaje"] = "No hay diagnósticos entre las fechas indicadas.";

                respuesta["Entidades"] = lista!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now;

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message;
                respuesta["Respuesta"] = "Error";
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        [HttpPost]
        public string ContarPorVehiculo()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!iAplicacionToken!.Validar(datos))
                {
                    respuesta["Error"] = "No autenticado.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                int idVehiculo = Convert.ToInt32(datos["idVehiculo"]);
                iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));

                int cantidad = iAplicacion.ContarPorVehiculo(idVehiculo);
                respuesta["Cantidad"] = cantidad;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now;

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message;
                respuesta["Respuesta"] = "Error";
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        [HttpPost]
        public string UltimoDiagnostico()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!iAplicacionToken!.Validar(datos))
                {
                    respuesta["Error"] = "No autenticado.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                var entidad = iAplicacion.UltimoDiagnostico();

                if (entidad == null)
                    respuesta["Mensaje"] = "No hay diagnósticos registrados.";

                respuesta["Entidad"] = entidad!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now;

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message;
                respuesta["Respuesta"] = "Error";
                return JsonConversor.ConvertirAString(respuesta);
            }
        }
    }
}
