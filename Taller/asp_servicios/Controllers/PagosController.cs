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
    public class PagosController : ControllerBase
    {
        private IPagosAplicacion? iAplicacion = null;
        private TokenAplicacion? iAplicacionToken = null;

        public PagosController(IPagosAplicacion? iAplicacion, TokenAplicacion iAplicacionToken)
        {
            this.iAplicacion = iAplicacion;
            this.iAplicacionToken = iAplicacionToken;
        }

        private Dictionary<string, object> ObtenerDatos()
        {
            var datos = new StreamReader(Request.Body).ReadToEnd().ToString();
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

                if (lista == null || lista.Count == 0)
                    respuesta["Mensaje"] = "No hay pagos registrados.";

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

                var entidad = JsonConversor.ConvertirAObjeto<Pagos>(
                    JsonConversor.ConvertirAString(datos["Entidad"]));

                if (entidad == null)
                {
                    respuesta["Error"] = "Entidad inválida.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                if (entidad.Id_factura <= 0)
                {
                    respuesta["Error"] = "Debe especificar una factura válida.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                if (entidad.Monto_total <= 0)
                {
                    respuesta["Error"] = "El monto del pago debe ser mayor a cero.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                if (entidad.Fecha_pago == DateTime.MinValue)
                    entidad.Fecha_pago = DateTime.Now;

                if (string.IsNullOrWhiteSpace(entidad.Estado))
                    entidad.Estado = "Pendiente";

                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                entidad = this.iAplicacion!.Guardar(entidad);

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
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                var entidad = JsonConversor.ConvertirAObjeto<Pagos>(
                    JsonConversor.ConvertirAString(datos["Entidad"]));

                if (entidad == null || entidad.Id <= 0)
                {
                    respuesta["Error"] = "Debe especificar un pago válido para modificar.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                if (entidad.Monto_total <= 0)
                {
                    respuesta["Error"] = "El monto del pago debe ser mayor a cero.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                if (string.IsNullOrWhiteSpace(entidad.Estado))
                    entidad.Estado = "Pendiente";

                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                entidad = this.iAplicacion!.Modificar(entidad);

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
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                var entidad = JsonConversor.ConvertirAObjeto<Pagos>(
                    JsonConversor.ConvertirAString(datos["Entidad"]));

                if (entidad == null || entidad.Id <= 0)
                {
                    respuesta["Error"] = "Debe especificar un pago válido para eliminar.";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                entidad = this.iAplicacion!.Borrar(entidad);

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
        public string PorFactura()
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

                int idFactura = Convert.ToInt32(datos["idFactura"]);
                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                var lista = this.iAplicacion!.PorFactura(idFactura);

                if (lista == null || lista.Count == 0)
                    respuesta["Mensaje"] = "No hay pagos asociados a esta factura.";

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
        public string PorEstado()
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

                string estado = datos["estado"].ToString()!;
                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                var lista = this.iAplicacion!.PorEstado(estado);

                if (lista == null || lista.Count == 0)
                    respuesta["Mensaje"] = "No hay pagos con el estado indicado.";

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
        public string PorFecha()
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

                DateTime fecha = DateTime.Parse(datos["fecha"].ToString()!);
                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                var lista = this.iAplicacion!.PorFecha(fecha);

                if (lista == null || lista.Count == 0)
                    respuesta["Mensaje"] = "No se registraron pagos en la fecha indicada.";

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
        public string TotalPagos()
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
                decimal total = this.iAplicacion!.TotalPagos();

                respuesta["Total"] = total;
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
        public string UltimoPago()
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
                var entidad = this.iAplicacion!.UltimoPago();

                if (entidad == null)
                    respuesta["Mensaje"] = "No hay pagos registrados.";

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
