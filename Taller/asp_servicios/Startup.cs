using asp_servicios.Controllers;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace asp_servicios
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration? Configuration { set; get; }

        public void ConfigureServices(WebApplicationBuilder builder, IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(x => {
                x.AllowSynchronousIO = true;
            });
            services.Configure<IISServerOptions>(x => { x.AllowSynchronousIO = true; });
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();

            //Repositorios
            services.AddScoped<IConexion, Conexion>();
            services.AddScoped<IReservasAplicacion, ReservasAplicacion>();
            services.AddScoped<IProductosAplicacion, ProductosAplicacion>();
            services.AddScoped<IReserva_ServicioAplicacion, Reserva_ServicioAplicacion>();
            services.AddScoped<IDetalles_ServicioAplicacion, Detalles_ServicioAplicacion>();
            services.AddScoped<IDetalles_ProductoAplicacion, Detalles_ProductoAplicacion>();
            services.AddScoped<IDetalles_RepuestoAplicacion, Detalles_RepuestoAplicacion>();
            services.AddScoped<IProveedoresAplicacion, ProveedoresAplicacion>();
            services.AddScoped<IDetalles_PagoAplicacion, Detalles_PagoAplicacion>();
            services.AddScoped<ISedesAplicacion, SedesAplicacion>();
            services.AddScoped<IClientesAplicacion, ClientesAplicacion>();
            services.AddScoped<IDiagnosticosAplicacion, DiagnosticosAplicacion>();
            services.AddScoped<IEmpleadosAplicacion, EmpleadosAplicacion>();
            services.AddScoped<IFacturasAplicacion, FacturasAplicacion>();
            services.AddScoped<IHerramientasAplicacion, HerramientasAplicacion>();
            services.AddScoped<IPagosAplicacion, PagosAplicacion>();
            services.AddScoped<IReparacionHerramientaAplicacion, ReparacionHerramientaAplicacion>();
            services.AddScoped<IReparacionesAplicacion, ReparacionesAplicacion>();
            services.AddScoped<IRepuestosAplicacion, RepuestosAplicacion>();
            services.AddScoped<IReservasAplicacion, ReservasAplicacion>();
            services.AddScoped<IServiciosAplicacion, ServiciosAplicacion>();
            services.AddScoped<IVehiculosAplicacion, VehiculosAplicacion>();
            services.AddScoped<IUsuariosAplicacion, UsuariosAplicacion>();
            services.AddScoped<TokenAplicacion, TokenAplicacion>();

            //Controladores
            services.AddScoped<TokenController, TokenController>();
            services.AddScoped<ReservasController, ReservasController>();
            services.AddScoped<ProductosController, ProductosController>();
            services.AddScoped<Reserva_ServicioController, Reserva_ServicioController>();
            services.AddScoped<Detalles_ServicioController, Detalles_ServicioController>();
            services.AddScoped<Detalles_ProductoController, Detalles_ProductoController>();
            services.AddScoped<Detalles_RepuestoController, Detalles_RepuestoController>();
            services.AddScoped<ProveedoresController, ProveedoresController>();
            services.AddScoped<Detalles_PagoController, Detalles_PagoController>();
            services.AddScoped<SedesController, SedesController>();
            services.AddScoped<ClientesController, ClientesController>();
            services.AddScoped<DiagnosticosController, DiagnosticosController>();
            services.AddScoped<EmpleadosController, EmpleadosController>();
            services.AddScoped<FacturasController, FacturasController>();
            services.AddScoped<HerramientasController, HerramientasController>();
            services.AddScoped<PagosController, PagosController>();
            services.AddScoped<Reparacion_HerramientaController, Reparacion_HerramientaController>();
            services.AddScoped<ReparacionesController, ReparacionesController>();
            services.AddScoped<RepuestosController, RepuestosController>();
            services.AddScoped<ReservasController, ReservasController>();
            services.AddScoped<ServiciosController, ServiciosController>();
            services.AddScoped<VehiculosController, VehiculosController>();
            services.AddScoped<UsuariosController, UsuariosController>();
            services.AddCors(o => o.AddDefaultPolicy(b => b.AllowAnyOrigin()));
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
            app.UseRouting();
            app.UseCors();
        }
    }
}