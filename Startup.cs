using Kidz.DatabaseConnection;
using Microsoft.EntityFrameworkCore;

namespace Kidz
{
     public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContex>(options =>
                options.UseSqlServer("server=192.168.0.133;Database=Kidz;User Id=sa;Password=anna12345"),ServiceLifetime.Scoped);
            services.AddSingleton((provider)=>
                {
                    return Configuration;
                });
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}