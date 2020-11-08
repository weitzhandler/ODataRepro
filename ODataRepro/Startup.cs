using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;

namespace ODataRepro
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<AppDbContext>();

            services.AddControllers();

            ConfigureOData(services);
        }

        void ConfigureOData(IServiceCollection services)
        {
            services.AddTransient<EdmModelBuilder>();
            services.AddTransient(serviceProvider =>
              serviceProvider.GetRequiredService<EdmModelBuilder>().GetEdmModel());

            services
              .AddOptions<ODataOptions>()
              .Configure<IEdmModel>((odataOptions, edmModel) =>
                odataOptions
                .SetAttributeRouting(true)
                .Select()
                .Expand()
                .Filter()
                .OrderBy()
                .SetMaxTop(128)
                .Count()
                .AddModel(edmModel));

            services.AddOData();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(routeBuilder =>
            {
                routeBuilder.MapControllers();
            });
        }
    }
}