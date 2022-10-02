using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Prototype.Application.Commands.Input.Documentos;
using Prototype.Application.Commands.Input.Servidores;
using Prototype.Application.Commands.Input.User;
using System.Reflection;

namespace Prototype.Api.ServicesResolve
{
    internal static class ResolveHandlers
    {
        internal static IServiceCollection AddHandlerDependency(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateServidorCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateServidorCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateDocumentoCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateDocumentoCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateUserCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateUserCommand).GetTypeInfo().Assembly);

            return services;
        }
    }
}
