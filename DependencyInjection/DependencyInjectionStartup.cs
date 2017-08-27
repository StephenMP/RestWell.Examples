using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestWell.Client;

namespace DependencyInjection
{
    public class DependencyInjectionStartup
    {
        public DependencyInjectionStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Configure our proxy however we want
            var proxyConfiguration = ProxyConfigurationBuilder
                                        .CreateBuilder()
                                        .Build();

            // You can either inject the ProxyConfiguration and new the proxy up each time yourself
            // (We recommmend injecting as a singleton)
            services.AddSingleton(proxyConfiguration);

            // Or you can inject the proxy
            services.AddSingleton<IProxy>(new Proxy(proxyConfiguration));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
