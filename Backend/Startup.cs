using System;
using AutoMapper;
using Backend.Data;
using Backend.Email;
using Backend.Handler;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace Backend
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

            var server = Configuration["DBServer"] ?? "localhost";
            var port = Configuration["DBPort"] ?? "14331";
            var user = Configuration["DBUser"] ?? "SA";
            var password = Configuration["DBPassword"] ?? "ThisIsNotSuperSecretP@55w0rd";
            var database = Configuration["Database"] ?? "Backend_API";

            System.Console.WriteLine($"Connection string: Server={server},{port};Initial Catalog={database};User ID={user};Password={password}");
            services.AddDbContext<BackendContext>(options => options.UseSqlServer($"Server={server},{port};Initial Catalog={database};User ID={user};Password={password}"));

            var emailServer = Configuration["SmtpServer"];
            var emailPort = int.Parse(Configuration["SmtpPort"]);
            var emailSsl = Boolean.Parse(Configuration["SSL"]);
            var emailUserName = Configuration["SmtpUserName"];
            var emailPassword = Configuration["SmtpUserPassword"];

            services.AddSingleton<IEmailConfiguration>(new EmailConfiguration() { SmtpServer = emailServer, SmtpPort = emailPort, Ssl = emailSsl, SmtpUserName = emailUserName, SmtpPassword = emailPassword });
            services.AddTransient<IEmailService, EmailService>();

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // Register the Swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Homebrewing Storage API",
                    Version = "v1"
                });
            });
            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IBackendRepo, SqlBackendRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Homebrewing Storage V1");
            });

            PrepDb.PrepPopulation(app);
        }
    }
}
