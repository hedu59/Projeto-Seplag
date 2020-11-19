
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Prototype.Api.DependencyInjection;
using Prototype.Domain.Interfaces;
using Prototype.Infra.Data;
using Prototype.Infra.Data.Interfaces;
using Prototype.Shared.Auth;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",builder => 
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Prototype API", Version = "v1" });
            });


            #region Token
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddDbContext<PrototypeDataContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("TokenConfig:HashKey").Value);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {

                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "prototype",
                    ValidAudience = "prototype",
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };


                options.Events = new JwtBearerEvents

                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Token Inválido...:" + context.Exception.Message.ToString());
                        return Task.CompletedTask;
                    },

                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token válido...:" + context.SecurityToken);
                        return Task.CompletedTask;
                    }

                };


            });

            #endregion

            

            DependencyInjection.ResoluteDependencies(services);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc().AddNewtonsoftJson(options => { options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver(); });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0); 
            services.AddResponseCompression();

            services.AddHttpContextAccessor(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prototype API V1");
            });
        }
    }
}
