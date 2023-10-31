using AutoMapper;
using Tarefas.Domain.Commads.ComandModels.EquipeColaborador;
using Tarefas.Domain.Commads.ComandModels.Login;
using Tarefas.Domain.Commads.ComandModels.Usuarios;
using Tarefas.Domain.Models.EquipeColaborador;
using Tarefas.Domain.Models.Usuario;
using Tarefas.Services.ViewModels.EquipeColaborador;
using Tarefas.Services.ViewModels.Login;
using Tarefas.Services.ViewModels.Usuarios;

namespace Tarefas.Services.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            //Usuário
            CreateMap<UsuarioViewModel, UsuarioModel>();
            CreateMap<UsuarioViewModel, UsuarioCreateCommand>();
            CreateMap<UsuarioViewModel, UsuarioEditCommand>();

            //Login
            CreateMap<LoginViewModel, AuthLoginCommand>();
            CreateMap<PrimeiroAcessoViewModel, AuthPrimeiroAcessoCommand>();

            //Galeria
            CreateMap<EquipeColaboradorViewModel, EquipeColaboradorCreateCommand>();
            CreateMap<EquipeColaboradorViewModel, EquipeColaboradorEditCommand>();
            CreateMap<EquipeColaboradorViewModel, EquipeColaboradorModel>();
        }
    }
}
