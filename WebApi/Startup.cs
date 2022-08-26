using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Common;
using WebApi.Models;
using WebApi.Sevices;

namespace WebApi
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
            services.AddControllers();
            services.AddScoped<IUserService, UserService>();// se agrega el servicio 
            services.Configure<BookSettings>(Configuration.GetSection(nameof(BookSettings)));// Agrega Setting connecion a MongoDB
            services.AddSingleton<IBookSettings>(d=>d.GetRequiredService<IOptions<BookSettings>>().Value);
            services.AddSingleton<ElementoService>();
            services.AddControllers();

            #region Settings JWT
            // Comienza configuración de JWT
            var appSettingsSection = Configuration.GetSection("AppSettings"); //Obtine la sección de la palabra secreta
            services.Configure<AppSettings>(appSettingsSection);

            // Aqui comienza JWT
            var appSettings = appSettingsSection.Get<AppSettings>();
            var llave = Encoding.ASCII.GetBytes(appSettings.Secreto);

            // se deben instalar los paquetes Microsoft.AspNetCore.Authentication.JwtBearer
            // y System.IdentityModel.Tokens.Jwt

            services.AddAuthentication(d =>
            {
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(d=>{
                    d.RequireHttpsMetadata = false;
                    d.SaveToken = true;
                    d.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(llave),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
            });
            #endregion
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

            app.UseAuthentication();//se habilita el JWT
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
