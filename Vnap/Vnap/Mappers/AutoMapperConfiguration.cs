using AutoMapper;
using Vnap.Entity;
using Vnap.Models;

namespace Vnap.Mappers
{
    using Plugin.DeviceInfo;

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
            public ServiceModelToModelMappingProfile()
            {
                CreateMap<Entity.Plant, Models.Plant>().ForMember(s => s.Avatar, o => o.MapFrom(d => ScaleImageUrl(d.Avatar)));
                CreateMap<Entity.Image, Models.Image>().ForMember(s => s.Url, o => o.MapFrom(d => ScaleImageUrl(d.Url)))
                    .ForMember(s => s.ThumbnailUrl, o => o.MapFrom(d => ScaleImageThumbnailUrl(d.Url)));
                CreateMap<Entity.PlantDisease, Models.PlantDisease>().ForMember(s => s.Avatar, o => o.MapFrom(d => ScaleImageUrl(d.Avatar)));
                CreateMap<Entity.Solution, Models.Solution>().ForMember(s => s.Avatar, o => o.MapFrom(d => ScaleImageUrl(d.Avatar)));
                CreateMap<ArticleEntity, Article>().ForMember(s => s.Description, o => o.MapFrom(d => d.Description.Length >= 80 ? d.Description.Substring(0, 80) + "..." : d.Description));
                CreateMap<AdvisoryMessageEntity, AdvisoryMessage>().ForMember(s => s.ImageUrl, o => o.MapFrom(d => ScaleImageUrl(d.ImageUrl)));
            }

            public static string ScaleImageUrl(string url)
            {
                var width = CrossDevice.Hardware.ScreenWidth;
                return url.Replace("upload/", $"upload/c_scale,w_{width}/");
            }

            public static string ScaleImageThumbnailUrl(string url)
            {
                return url.Replace("upload/", $"upload/c_scale,w_100/");
            }
        }

        public class ModelToServiceModelMappingProfile : Profile
        {
            public ModelToServiceModelMappingProfile()
            {
                CreateMap<Models.Plant, Entity.Plant>();
                CreateMap<Models.PlantDisease, Entity.PlantDisease>();
                CreateMap<Models.Solution, Entity.Solution>();
                CreateMap<Article, ArticleEntity>();
                CreateMap<AdvisoryMessage, AdvisoryMessageEntity>();
                CreateMap<Models.Image, Entity.Image>();
            }
        }
    }
}
