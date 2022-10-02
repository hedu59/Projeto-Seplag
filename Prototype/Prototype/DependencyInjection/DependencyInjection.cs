using Microsoft.Extensions.DependencyInjection;
using Prototype.Application.Filas.Producers;
using Prototype.Application.Handlers;
using Prototype.Application.Interfaces;
using Prototype.Application.Interfaces.Filas;
using Prototype.Application.Services;
using Prototype.Domain.Interfaces.IUnitOfWork;
using Prototype.Infra.Data;
using Prototype.Infra.Data.Interfaces;
using Prototype.Infra.Data.UnitOfWork;
using Prototype.Shared.Auth;

namespace Prototype.Api.DependencyInjection
{
    public static class ResolveDependencyInjection
    {
 
        public static void ResolveDependencies(IServiceCollection services)
        {
            ServicesDependencies(services);
            HandlresDependecies(services);
            FilasDependencies(services);
        }

        static void FilasDependencies(IServiceCollection services)
        {
            services.AddScoped<ILogTransacaoProducer, LogTransacaoProducer>();
        }

        static void ServicesDependencies(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IServidorService, ServidorService>();
            services.AddScoped<IDocumentoService, DocumentoService>();
            services.AddScoped<IUnitOfWork, UnitOfWork<PrototypeDataContext>>();
            services.AddScoped<IUser, UserNet>();
        }

        static void HandlresDependecies(IServiceCollection services)
        {
            services.AddScoped<ServidorHandler, ServidorHandler>();
            services.AddScoped<DocumentoHandler, DocumentoHandler>();
        }
    }



}
