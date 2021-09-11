using System.Data;
using AutoMapper;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.InputModels.Utenti;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.Mapping.Resolvers;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.ViewModels.Profili;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.ViewModels.Utenti;

namespace Template_SQLite_AdoNet_Crud_AutoMapper.Models.Mapping
{
    public class AdoNetUtenteProfile : Profile
    {
        public AdoNetUtenteProfile()
        {
            CreateMap<DataRow, UtenteViewModel>()
                .ForMember(viewModel => viewModel.Id, config => config.MapFrom(IdResolver.Instance))
                .ForAllOtherMembers(config => config.MapFrom(DefaultResolver.Instance, dataRow => config.DestinationMember.Name));

            CreateMap<DataRow, UtenteDetailViewModel>()
                .ForMember(viewModel => viewModel.Id, config => config.MapFrom(IdResolver.Instance))
                .ForMember(viewModel => viewModel.Profili, config => config.Ignore())
                .ForAllOtherMembers(config => config.MapFrom(DefaultResolver.Instance, dataRow => config.DestinationMember.Name));

            CreateMap<DataRow, UtenteEditInputModel>()
                .ForMember(viewModel => viewModel.Id, config => config.MapFrom(IdResolver.Instance))
                .ForAllOtherMembers(config => config.MapFrom(DefaultResolver.Instance, dataRow => config.DestinationMember.Name));

            CreateMap<DataRow, UtenteViewModel>()
                .ForMember(viewModel => viewModel.Id, config => config.MapFrom(IdResolver.Instance))
                .ForAllOtherMembers(config => config.MapFrom(DefaultResolver.Instance, dataRow => config.DestinationMember.Name));
        }
    }
}