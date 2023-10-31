using AutoMapper;
using Tarefas.Domain.Models.EquipeColaborador;
using Tarefas.Domain.Models.Usuario;
using Tarefas.Services.ViewModels.EquipeColaborador;
using Tarefas.Services.ViewModels.Usuarios;

namespace Tarefas.Services.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            //Usuário
            CreateMap<UsuarioModel, UsuarioViewModel>();

            // Galeria
            CreateMap<EquipeColaboradorModel, EquipeColaboradorViewModel>();
        }
    }
}