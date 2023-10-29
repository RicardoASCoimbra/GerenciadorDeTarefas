

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Tarefas.Domain.Commads.ComandModels.Usuarios;
using Tarefas.Domain.Core.Interfaces;
using Tarefas.Domain.Core.Notifications;
using Tarefas.Domain.Interfaces.Infra.Data;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.Usuarios;
using Tarefas.Infra.Bus;
using Tarefas.Infra.Data.Configuration;
using Tarefas.Infra.Data.Context;
using Tarefas.Infra.Data.EventSourcing;
using Tarefas.Infra.Data.Repositories.Usuarios;
using Tarefas.Services.AppServices.Usuarios;
using Tarefas.Services.Interfaces.Usuarios;

namespace Tarefas.Infra.IoC
{
    public class NativeInjector
    {
        public static void RegisterAppServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStore, EventStore>();
            services.AddDbContext<TarefaDB>();
            services.AddScoped<TarefaDB>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //-----------------------------------------------------------------------------------------------------------------------------
            ///Usuario
            //----------------------------------------------------------------------------------------------------------------------

            //repository
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            //Service 
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();

            // Command
            services.AddScoped<IRequestHandler<UsuarioCreateCommand, Unit>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<UsuarioEditCommand, Unit>, UsuarioCommandHandler>();
            //services.AddScoped<IRequestHandler<AutenticacaoLoginCommand, Unit>, AutenticacaoCommandHandler>();
            //services.AddScoped<IRequestHandler<AutenticacaoPrimeiroAcessoCommand, Unit>, AutenticacaoCommandHandler>();

            //        //---------------------------------------------------------------------------------------------------------------
            //        ///LOGINPRINCIPAL
            //        //__________________________________________________________________________________________________

            //        //Gerenciador - Service
            //        services.AddScoped<ILoginPrincipalAppService, LoginPrincipalAppService>();

            //        //Comand
            //        services.AddScoped<IRequestHandler<AutenticacaoLoginPrincipalCommand, Unit>, AutenticacaoPrincipalCommandHandler>();
            //        services.AddScoped<IRequestHandler<AutenticacaoPrimeiroAcessoPrincipalCommand, Unit>, AutenticacaoPrincipalCommandHandler>();

            //    }
            //}
        }
    }
}