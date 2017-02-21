using AutoMapper;
using Vnap.Entity;
using Vnap.Models;

namespace Vnap.Mappers
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<ServiceModelToModelMappingProfile>();
                x.AddProfile<ModelToServiceModelMappingProfile>();
            });
        }

        public class ServiceModelToModelMappingProfile : Profile
        {
            public override string ProfileName
            {
                get { return "ServiceModelToModelMappingProfile"; }
            }

            protected override void Configure()
            {
                CreateMap<Entity.Plant, Models.Plant>();
                CreateMap<Entity.Image, Models.Image>();
                CreateMap<Entity.PlantDisease, Models.PlantDisease>();
                CreateMap<Entity.Solution, Models.Solution>();
                CreateMap<ArticleEntity, Article>().ForMember(p => p.Description, o => o.MapFrom(pe => pe.Description.Length >= 80 ? pe.Description.Substring(0, 80) + "..." : pe.Description));
                CreateMap<AdvisoryMessageEntity, AdvisoryMessage>();
            }
        }

        public class ModelToServiceModelMappingProfile : Profile
        {
            public override string ProfileName
            {
                get { return "ModelToServiceModelMappingProfile"; }
            }

            protected override void Configure()
            {
                CreateMap<Models.Plant, Entity.Plant>();
                CreateMap<Models.PlantDisease, Entity.PlantDisease>();
                CreateMap<Models.Solution, Entity.Solution>();
                CreateMap<Article, ArticleEntity>();
                CreateMap<Models.Image, Entity.Image>();
            }
        }
    }
}
