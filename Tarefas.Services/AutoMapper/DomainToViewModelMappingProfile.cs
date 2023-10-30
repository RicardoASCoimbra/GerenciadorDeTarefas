using AutoMapper;
using Tarefas.Domain.Models.Usuario;
using Tarefas.Services.ViewModels.Usuarios;

namespace Tarefas.Services.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            //Usuário
            CreateMap<UsuarioModel, UsuarioViewModel>();

            ////Equipe do colaborador
            //CreateMap<ColaboradoresModel, ColaboradorViewModel>();
            //CreateMap<EquipeColaboradorModels, EquipesViewModel>();

        }
    }
}