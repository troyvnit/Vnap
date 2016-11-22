using AutoMapper;
using Vnap.Web.DataAccess.Entity;
using Vnap.Web.ViewModels;

namespace Vnap.Web.Mappers
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<ViewModelToModelMappingProfile>();
                x.AddProfile<ModelToViewModelMappingProfile>();
            });
        }

        public class ViewModelToModelMappingProfile : Profile
        {
            public override string ProfileName
            {
                get { return "ViewModelToModelMappingProfile"; }
            }

            protected override void Configure()
            {
                CreateMap<PlantVM, Plant>().ForMember(p => p.CreatedDate, o => o.Ignore());
                CreateMap<PlantDiseaseVM, PlantDisease>().ForMember(p => p.CreatedDate, o => o.Ignore());
                CreateMap<SolutionVM, Solution>().ForMember(p => p.CreatedDate, o => o.Ignore());
                CreateMap<ImageVM, Image>().ForMember(p => p.CreatedDate, o => o.Ignore());
                CreateMap<AdvisoryMessageVM, AdvisoryMessage>().ForMember(p => p.CreatedDate, o => o.Ignore());
            }
        }

        public class ModelToViewModelMappingProfile : Profile
        {
            public override string ProfileName
            {
                get { return "ModelToViewModelMappingProfile"; }
            }

            protected override void Configure()
            {
                CreateMap<Plant, PlantVM>();
                CreateMap<PlantDisease, PlantDiseaseVM>().ForMember(pd => pd.PlantName, o => o.MapFrom(pd => pd.Plant != null ? pd.Plant.Name : string.Empty));
                CreateMap<Solution, SolutionVM>().ForMember(pd => pd.PlantDiseaseName, o => o.MapFrom(pd => pd.PlantDisease != null ? pd.PlantDisease.Name : string.Empty));
                CreateMap<Image, ImageVM>();
                CreateMap<AdvisoryMessage, AdvisoryMessageVM>();
            }
        }
    }
}
