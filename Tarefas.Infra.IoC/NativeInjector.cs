

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Tarefas.Domain.Commads.Autenticacao;
using Tarefas.Domain.Commads.ComandModels.Login;
using Tarefas.Domain.Commads.ComandModels.Tarefa;
using Tarefas.Domain.Commads.ComandModels.Usuarios;
using Tarefas.Domain.Core.Interfaces;
using Tarefas.Domain.Core.Notifications;
using Tarefas.Domain.Interfaces.Infra.Data;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.EquipeColaborador;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.Tarefa;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.Usuarios;
using Tarefas.Infra.Bus;
using Tarefas.Infra.Data.Configuration;
using Tarefas.Infra.Data.Context;
using Tarefas.Infra.Data.EventSourcing;
using Tarefas.Infra.Data.Repositories.EquipeColaborador;
using Tarefas.Infra.Data.Repositories.Tarefa;
using Tarefas.Infra.Data.Repositories.Usuarios;
using Tarefas.Services.AppServices.Login;
using Tarefas.Services.AppServices.Tarefa;
using Tarefas.Services.AppServices.Usuarios;
using Tarefas.Services.Interfaces.Login;
using Tarefas.Services.Interfaces.Tarefa;
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
            services.AddScoped<IEquipeColaboradorRepository, EquipeColaboradorRepository>();

            //Service 
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();

            // Command
            services.AddScoped<IRequestHandler<UsuarioCreateCommand, Unit>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<UsuarioEditCommand, Unit>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<AutenticacaoLoginCommand, Unit>, AutenticacaoCommandHandler>();
            services.AddScoped<IRequestHandler<AutenticacaoPrimeiroAcessoCommand, Unit>, AutenticacaoCommandHandler>();


            //---------------------------------------------------------------------------------------------------------------
            ///LOGIN
            //__________________________________________________________________________________________________

            // Service
            services.AddScoped<ILoginAppService, LoginAppService>();

            //Comand
            services.AddScoped<IRequestHandler<AuthLoginCommand, Unit>, AulhCommandHandler>();
            services.AddScoped<IRequestHandler<AuthPrimeiroAcessoCommand, Unit>, AulhCommandHandler>();
            //---------------------------------------------------------------------------------------------------------------
            ///TermoAceite
            //__________________________________________________________________________________________________

            //SERVICE
            services.AddScoped<ITarefaRepository, TarefaRepository>();
            services.AddScoped<ITarefaAppService, TarefaAppService>();

            //COMMAND
            services.AddScoped<IRequestHandler<TarefaCreateCommand, Unit>, TarefaCommandHandler>();
            services.AddScoped<IRequestHandler<TarefaEditCommand, Unit>, TarefaCommandHandler>();



        }
    }
}