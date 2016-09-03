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
                CreateMap<Post, PostEntity>();
            }
        }
    }
}
