using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Prototype.Application.Interfaces;
using Prototype.Application.Services;
using Prototype.Domain.Entities;
using Prototype.Domain.Handlers;
using Prototype.Domain.Interfaces;
using Prototype.Domain.Interfaces.IUnitOfWork;
using Prototype.Domain.Interfaces.Repositories;
using Prototype.Infra.Data;
using Prototype.Infra.Data.Interfaces;
using Prototype.Infra.Data.Repositories;
using Prototype.Infra.Data.UnitOfWork;
using Prototype.Shared.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prototype.Api.DependencyInjection
{
    public static class ResolveDependencyInjection
    {
 
        public static void ResolveDependencies(IServiceCollection services)
        {
            ServicesDependencies(services);
            HandlresDependecies(services);
            MongoDependencies(services);
        }

        static void MongoDependencies(IServiceCollection services)
        {
            services.AddScoped<ITransacaoMongoRepository, TransacaoMongoRepository>();
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
            services.AddScoped<BeneficioServidorHandler, BeneficioServidorHandler>();
            services.AddScoped<DocumentoHandler, DocumentoHandler>();
        }
    }



}
