using AutoMapper;
using Tarefas.Domain.Commads.ComandModels.Login;
using Tarefas.Domain.Commads.ComandModels.Usuarios;
using Tarefas.Domain.Models.Usuario;
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

            ////Equipe do Colaborador
            //CreateMap<EquipesViewModel, EquipesCreateCommand>();
            //CreateMap<EquipesViewModel, EquipesEditCommand>();
            //CreateMap<EquipesViewModel, EquipeColaboradorModels>();
        }
    }
}
