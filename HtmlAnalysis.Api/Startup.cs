using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAnalysis.Data;
using HtmlAnalysis.Data.Queries;
using HtmlAnalysis.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace HtmlAnalysis.Api
{
    public class Startup
    {
        readonly string AllowSpecificOrigins = "_AllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost", 
                            "https://localhost:44383", "http://localhost:44383")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    });
            });
            services.AddSwaggerGen(options => 
            {
                options.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Swagger Html Analysis Api",
                        Description = "Html Analysis Api",
                        Version = "v1"
                    });
            });
            services.AddControllers();
            services.AddTransient<IWordCountSvc, WordCountSvc>();
            services.AddTransient<ICommandText, CommandText>();
            services.AddTransient<IWordCountRepository, WordCountRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Html Analysis Api");
                options.RoutePrefix = "";
            });
        }        
    }
}
