using System.Data;
using AutoMapper;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.InputModels.Profili;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.Mapping.Resolvers;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.ViewModels.Profili;

namespace Template_SQLite_AdoNet_Crud_AutoMapper.Models.Mapping
{
    public class AdoNetProfiloProfile : Profile
    {
        public AdoNetProfiloProfile()
        {
            CreateMap<DataRow, ProfiloViewModel>()
                .ForMember(viewModel => viewModel.Id, config => config.MapFrom(IdResolver.Instance))
                .ForAllOtherMembers(config => config.MapFrom(DefaultResolver.Instance, dataRow => config.DestinationMember.Name));

            CreateMap<DataRow, ProfiloDetailViewModel>()
                .ForMember(viewModel => viewModel.Id, config => config.MapFrom(IdResolver.Instance))
                .ForMember(viewModel => viewModel.UtenteId, config => config.MapFrom(UtenteIdResolver.Instance))
                .ForAllOtherMembers(config => config.MapFrom(DefaultResolver.Instance, dataRow => config.DestinationMember.Name));

            CreateMap<DataRow, ProfiloEditInputModel>()
                .ForMember(viewModel => viewModel.Id, config => config.MapFrom(IdResolver.Instance))
                .ForMember(viewModel => viewModel.UtenteId, config => config.MapFrom(UtenteIdResolver.Instance))
                .ForAllOtherMembers(config => config.MapFrom(DefaultResolver.Instance, dataRow => config.DestinationMember.Name));

            CreateMap<DataRow, ProfiloViewModel>()
                .ForMember(viewModel => viewModel.Id, config => config.MapFrom(IdResolver.Instance))
                .ForAllOtherMembers(config => config.MapFrom(DefaultResolver.Instance, dataRow => config.DestinationMember.Name));
        }
    }
}