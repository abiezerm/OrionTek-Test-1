using ClientDirectory.Common.Services;
using ClientDirectory.Common.Services.Contracts;
using ClientDirectory.Configuration;
using ClientDirectory.DataAccess.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClientDirectory
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
            services.AddCors(o => o.AddPolicy("EveryOne", builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            }));

            services.AddAuthentication(Configuration);

            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddRespositories(Configuration);

            services.AddControllers().AddNewtonsoftJson(options =>            
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("EveryOne");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
