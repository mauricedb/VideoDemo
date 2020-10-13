using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace VideoDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(configurePolicy =>
            {
                configurePolicy.AllowAnyOrigin();
                configurePolicy.AllowAnyMethod();
                configurePolicy.AllowAnyHeader();
            });
            app.UseDefaultFiles();

            var contentTypeProvider = new FileExtensionContentTypeProvider();
            contentTypeProvider.Mappings[".mpd"] = "application/dash+xml";
            contentTypeProvider.Mappings[".m4s"] = "audio/mp4";
            app.UseStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = contentTypeProvider
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
