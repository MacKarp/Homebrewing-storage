using System;
using System.Text;
using AutoMapper;
using Backend.Data;
using Backend.Email;
using Backend.Jobs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Quartz;

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
            services.AddCors();

            var server = Configuration["DBServer"] ?? "localhost";
            var port = Configuration["DBPort"] ?? "14331";
            var user = Configuration["DBUser"] ?? "SA";
            var password = Configuration["DBPassword"] ?? "ThisIsNotSuperSecretP@55w0rd";
            var database = Configuration["Database"] ?? "Backend_API";

            System.Console.WriteLine($"Connection string: Server={server},{port};Initial Catalog={database};User ID={user};Password={password}");
            services.AddDbContext<BackendContext>(options => options.UseSqlServer($"Server={server},{port};Initial Catalog={database};User ID={user};Password={password}"));

            var emailServer = Configuration["SmtpServer"] ?? "DefaultEmailServer";
            var emailPort = (Configuration["SmtpPort"]) ?? "0";
            var emailSsl = (Configuration["SSL"]) ?? "true";
            var emailUserName = Configuration["SmtpUserName"] ?? "DefaultUserName";
            var emailPassword = Configuration["SmtpUserPassword"] ?? "DefaultUserPassword";

            services.AddSingleton<IEmailConfiguration>(new EmailConfiguration() { SmtpServer = emailServer, SmtpPort = int.Parse(emailPort), Ssl = Boolean.Parse(emailSsl), SmtpUserName = emailUserName, SmtpPassword = emailPassword });
            services.AddTransient<IEmailService, EmailService>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<BackendContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Token configuration1
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                    ClockSkew = TimeSpan.Zero
                }) ;

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

            var notificationSchedule = Configuration["NotificationSchedule"] ?? "0/30 * * * * ?";
            Console.WriteLine("NotificationSchedule: " + notificationSchedule);
            services.AddQuartz(q =>
            {
                // base quartz scheduler, job and trigger configuration
                q.UseMicrosoftDependencyInjectionScopedJobFactory();


                // Create a "key" for the job
                var jobKey = new JobKey("NotificationEmailSend");

                // Register the job with the DI container
                q.AddJob<NotificationEmailSend>(opts => opts.WithIdentity(jobKey));

                // Create a trigger for the job
                q.AddTrigger(opts => opts
                    .ForJob(jobKey) // link to the NotificationEmailSend
                    .WithIdentity("NotificationEmailSend") // give the trigger a unique name
                    .WithCronSchedule(notificationSchedule)); // run with "NotificationSchedule" value or every 30 seconds
            });

            // ASP.NET Core hosting
            services.AddQuartzServer(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
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
