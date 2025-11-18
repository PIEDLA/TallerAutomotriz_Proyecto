using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;

namespace asp_presentacion
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
            // Presentaciones
            services.AddScoped<ISedesPresentacion, SedesPresentacion>();
            services.AddScoped<IReservasPresentacion, ReservasPresentacion>();
            services.AddScoped<IReserva_ServicioPresentacion, Reserva_ServicioPresentacion>();
            services.AddScoped<IProveedoresPresentacion, ProveedoresPresentacion>();
            services.AddScoped<IProductosPresentacion, ProductosPresentacion>();
            services.AddScoped<IDetalles_ServicioPresentacion, Detalles_ServicioPresentacion>();
            services.AddScoped<IDetalles_RepuestoPresentacion, Detalles_RepuestoPresentacion>();
            services.AddScoped<IDetalles_ProductoPresentacion, Detalles_ProductoPresentacion>();
            services.AddScoped<IDetalles_PagoPresentacion, Detalles_PagoPresentacion>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddRazorPages();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.UseSession();
            app.Run();
        }
    }
}