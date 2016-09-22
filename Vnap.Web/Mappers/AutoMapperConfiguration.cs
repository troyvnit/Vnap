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
                CreateMap<PlantVM, Plant>();
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
            }
        }
    }
}
