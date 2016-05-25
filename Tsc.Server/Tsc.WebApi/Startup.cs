using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tsc.Application.Repositories;
using Tsc.DataAccess;
using Tsc.WebApi.ServiceModel;

namespace Tsc.WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("config.user.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            // should be configured like this:
            //services.Configure<CorsOptions>(options => Configuration.GetSection("CorsOptions").Bind(options));

            services.Configure<MongoRestTscDataAccessConfiguration>(options => Configuration.GetSection("MongoRestTscDataAccess").Bind(options));
            services.AddApplicationDependencies(Convert.ToBoolean(Configuration["ApplicationDependencies:UsePersistenRepos"]));

            services.AddIdentity<User, Role>(options =>
            {
                // test only
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            });

            services.AddTransient<IUserStore<User>, UserStore<User>>();
            services.AddTransient<IRoleStore<Role>, RoleStore<Role>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentity();
            app.UseCors("AllowAll");

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "api/{controller=Test}/{action=About}");
            });
        }
    }

    public class CorsRenderer
    {
        private readonly IOptions<CorsOptions> _corsOptions;

        public CorsRenderer(IOptions<CorsOptions> corsOptions)
        {
            _corsOptions = corsOptions;
        }

        public void Render()
        {
            string serializeObject = JsonConvert.SerializeObject(_corsOptions.Value);
            Console.WriteLine(serializeObject);
        }
    }
}
