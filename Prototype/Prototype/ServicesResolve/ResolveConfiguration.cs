
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prototype.Api.DependencyInjection;
using Prototype.Infra.Data;
using Prototype.Infra.Data.MongoConfiguration;

namespace Prototype.Api.ServicesResolve
{
    internal static class ResolveConfiguration
    {
        internal static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            ResolveDependencyInjection.ResolveDependencies(services);
            services.Configure<DbConfiguration>(configuration.GetSection("MongoDbConnection"));
            services.AddDbContext<PrototypeDataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc().AddNewtonsoftJson(options => { options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver(); });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddResponseCompression();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
