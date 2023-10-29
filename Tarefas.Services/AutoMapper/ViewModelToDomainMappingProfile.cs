using AutoMapper;
using Tarefas.Domain.Commads.ComandModels.Usuarios;
using Tarefas.Domain.Models.Usuario;
using Tarefas.Services.ViewModels.Usuarios;

namespace Tarefas.Services.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            //Usuário
            CreateMap<UsuarioViewModel, Usuario>();
            CreateMap<UsuarioViewModel, UsuarioCreateCommand>();
            CreateMap<UsuarioViewModel, UsuarioEditCommand>();
        }
    }
}
