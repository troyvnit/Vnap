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
                CreateMap<PostEntity, Post>().ForMember(p => p.Description, o => o.MapFrom(pe => pe.Description.Substring(0, 50) + "..."));
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
                CreateMap<Post, PostEntity>();
                CreateMap<Models.Image, Entity.Image>();
            }
        }
    }
}
