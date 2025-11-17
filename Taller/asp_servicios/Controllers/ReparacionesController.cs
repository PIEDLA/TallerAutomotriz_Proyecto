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
    public class ReparacionesController : ControllerBase
    {
        private readonly IReparacionesAplicacion? iAplicacion;
        private readonly TokenAplicacion? iAplicacionToken;

        public ReparacionesController(IReparacionesAplicacion? iAplicacion, TokenAplicacion iAplicacionToken)
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
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                var lista = this.iAplicacion!.Listar();

                respuesta["Entidades"] = lista!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
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
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                var entidad = JsonConversor.ConvertirAObjeto<Reparaciones>(
                    JsonConversor.ConvertirAString(datos["Entidad"]));

                if (entidad == null)
                {
                    respuesta["Error"] = "Entidad inválida.";
                    return JsonConversor.ConvertirAString(respuesta);
                }


                if (entidad.Id_diagnostico <= 0)
                {
                    respuesta["Error"] = "Debe asociar un diagnóstico válido.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                if (string.IsNullOrWhiteSpace(entidad.Descripcion_trabajo))
                {
                    respuesta["Error"] = "La descripción del trabajo es obligatoria.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                if (entidad.Costo_estimado <= 0)
                {
                    respuesta["Error"] = "El costo estimado debe ser mayor a cero.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                if (entidad.Fecha_inicio == DateTime.MinValue)
                    entidad.Fecha_inicio = DateTime.Now;

                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                entidad = this.iAplicacion!.Guardar(entidad);

                respuesta["Entidad"] = entidad!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
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
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                var entidad = JsonConversor.ConvertirAObjeto<Reparaciones>(
                    JsonConversor.ConvertirAString(datos["Entidad"]));

                if (entidad == null || entidad.Id <= 0)
                {
                    respuesta["Error"] = "Reparación inválida para modificar.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                entidad = this.iAplicacion!.Modificar(entidad);

                respuesta["Entidad"] = entidad!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
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
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                var entidad = JsonConversor.ConvertirAObjeto<Reparaciones>(
                    JsonConversor.ConvertirAString(datos["Entidad"]));

                if (entidad == null || entidad.Id <= 0)
                {
                    respuesta["Error"] = "Reparación inválida para eliminar.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                entidad = this.iAplicacion!.Borrar(entidad);

                respuesta["Entidad"] = entidad!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
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
        public string PorDiagnostico()
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

                int idDiagnostico = Convert.ToInt32(datos["IdDiagnostico"]);
                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                var lista = this.iAplicacion!.PorDiagnostico(idDiagnostico);

                respuesta["Entidades"] = lista!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
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
        public string Costosas()
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

                decimal minimo = Convert.ToDecimal(datos["Minimo"]);
                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                var lista = this.iAplicacion!.Costosas(minimo);

                respuesta["Entidades"] = lista!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
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
        public string EntreFechas()
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

                DateTime inicio = DateTime.Parse(datos["Inicio"].ToString()!);
                DateTime fin = DateTime.Parse(datos["Fin"].ToString()!);

                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                var lista = this.iAplicacion!.EntreFechas(inicio, fin);

                respuesta["Entidades"] = lista!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
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
        public string UltimaReparacion()
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

                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                var reparacion = this.iAplicacion!.UltimaReparacion();

                respuesta["Entidad"] = reparacion!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
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
        public string TotalEstimado()
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

                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                decimal total = this.iAplicacion!.TotalEstimado();

                respuesta["TotalEstimado"] = total;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
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
