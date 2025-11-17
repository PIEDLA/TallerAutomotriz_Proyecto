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

            // Repositorios
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
            services.AddScoped<IVehiculosAplicacion, VehiculosAplicacion>();
            services.AddScoped<IEmpleadosAplicacion, EmpleadosAplicacion>();
            services.AddScoped<IServiciosAplicacion, ServiciosAplicacion>();
            services.AddScoped<IFacturasAplicacion, FacturasAplicacion>();
            services.AddScoped<TokenAplicacion, TokenAplicacion>();
            // Controladores
            services.AddScoped<TokenController, TokenController>();
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