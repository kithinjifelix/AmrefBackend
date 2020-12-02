using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PG.Management.Core;
using PG.Management.Infrastructure;
using PG.Management.Infrastructure.Persistence;
using PG.SharedKernel.Infrastructure.Persistence;
using Serilog;

namespace PG.Portal.Management
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        private static string[] _allowedOrigins;

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _allowedOrigins = Configuration.GetSection("AllowedOrigins").Get<string[]>();

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.SetIsOriginAllowed(isOriginAllowed: _ => true).AllowAnyHeader().AllowAnyMethod()
                            .AllowCredentials();
                    });
            });

            services.AddControllers();
            services.AddInfrastructure(Configuration);
            services.AddApplication();
            services.AddSwaggerGen();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseForwardedHeaders();
            }
            else
            {
                app.UseForwardedHeaders();
                app.UseHsts();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PG.Management V1");
            });
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            EnsureMigrationOfContext<ProjectContext>(app);
            Log.Information($"PG.Management [Version {GetType().Assembly.GetName().Version}] started successfully");
        }


        private static void EnsureMigrationOfContext<T>(IApplicationBuilder app) where T : BaseContext
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var contextName = typeof(T).Name;
                Log.Debug($"initializing Database context: {contextName}");
                var context = serviceScope.ServiceProvider.GetService<T>();
                try
                {
                    context.Database.Migrate();
                    context.EnsureSeeded();
                    Log.Debug($"initializing Database context: {contextName} [OK]");
                }
                catch (Exception e)
                {
                    var msg = $"initializing Database context: {contextName} Error";
                    Log.Error(e, msg);
                }
            }
        }
    }
}
