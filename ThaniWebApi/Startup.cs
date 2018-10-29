using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using ThaniWebApi.Controllers.Points;
using Insight.Database;
using Insight.Database.Json;
using Microsoft.AspNetCore.Server.IISIntegration;
using ZNetCS.AspNetCore.Authentication.Basic;
using ZNetCS.AspNetCore.Authentication.Basic.Events;
using System.Security.Claims;
using ThaniWebApi.Attributes;

namespace ThaniWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Basic Authentication  --https://github.com/msmolka/ZNetCS.AspNetCore.Authentication.Basic
            //Install - Package ZNetCS.AspNetCore.Authentication.Basic
            services.AddScoped<AuthenticationEvents>();

            services
                .AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
                .AddBasicAuthentication(
                    options =>
                    {
                        options.Realm = "ThaniWebApi"; //"My Application";
                        options.EventsType = typeof(AuthenticationEvents);
        
                    });


            // IIServer Defaults requires the following import:
            // using Microsoft.AspNetCore.Server.IISIntegration;
            // services.AddAuthentication(IISDefaults.AuthenticationScheme);

            //Kestral Server: HttpSysDefaults requires the following import:
            // using Microsoft.AspNetCore.Server.HttpSys;
            // services.AddAuthentication(HttpSysDefaults.AuthenticationScheme);

            //-----------------------------------------------------------------
            //Declare all interface for WebApi and their dataAcess pairing
            //-----------------------------------------------------------------
            //services.AddTransient<IPointsDataProvider, PointsDataAccess>();
            services.AddTransient<IPointsRepository, PointsDataAccess>();

            //Set [ApiController] as default
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //use conpression 
            //PM: Install-Package Microsoft.AspNetCore.ResponseCompression -Version 2.1.1
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            //CORS services 
            //PM: Install-Package Microsoft.AspNetCore.Cors -Version 2.1.1
            services.AddCors();


            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ThaniPointsAPI", Version = "v1" });
            });

            // at some point in application startup Insight.Database... --https://github.com/jonwagner/Insight.Database/wiki/Executing-SQL-Commands
            //PM: Install-Package Insight.Database
            SqlInsightDbProvider.RegisterProvider();
            JsonNetObjectSerializer.Initialize(); //Insight.Database.Json Class[Column(SerializationMode=SerializationMode.Json)]
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MassyPoints API V1");
            });


            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            //app.UseCors(builder =>
            //       builder.WithOrigins("http://localhost:54574")
            //       .AllowAnyHeader()
            //       //.AllowAnyMethod()
            //      // .AllowCredentials()
            //);

            app.UseResponseCompression();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            // default authentication initialization
            app.UseAuthentication();


            //app.UseMvc();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
