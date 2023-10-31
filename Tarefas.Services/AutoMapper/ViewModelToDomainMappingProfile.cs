using AutoMapper;
using Tarefas.Domain.Commads.ComandModels.Login;
using Tarefas.Domain.Commads.ComandModels.Tarefa;
using Tarefas.Domain.Commads.ComandModels.Usuarios;
using Tarefas.Domain.Models.EquipeColaborador;
using Tarefas.Domain.Models.Tarefa;
using Tarefas.Domain.Models.Usuario;
using Tarefas.Services.ViewModels.EquipeColaborador;
using Tarefas.Services.ViewModels.Login;
using Tarefas.Services.ViewModels.Tarefa;
using Tarefas.Services.ViewModels.Usuarios;

namespace Tarefas.Services.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            //Usuário
            CreateMap<UsuarioModel, UsuarioViewModel>();
            CreateMap<EquipeColaboradorModel, EquipeColaboradorViewModel>();
            CreateMap<UsuarioViewModel, UsuarioModel>();
            CreateMap<UsuarioViewModel, UsuarioCreateCommand>();
            CreateMap<UsuarioViewModel, UsuarioEditCommand>();
            CreateMap<EquipeColaboradorViewModel, EquipeColaboradorModel>();
            CreateMap<EquipeColaboradorViewModel, UsuarioCreateCommand>();
            CreateMap<EquipeColaboradorViewModel, UsuarioEditCommand>();

            //Login
            CreateMap<LoginViewModel, AuthLoginCommand>();
            CreateMap<PrimeiroAcessoViewModel, AuthPrimeiroAcessoCommand>();

            //Tarefa
            CreateMap<TarefaViewModel, TarefaCreateCommand>();
            CreateMap<TarefaViewModel, TarefaEditCommand>();
            CreateMap<TarefaViewModel, TarefaModel>();

        }
    }
}
