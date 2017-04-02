using AutoMapper;
using LiveLessons.BLL.Infrastructure.AutomapperRegistration;

namespace LiveLesson.WEB.AutomapperRegistrations
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new ViewModelToDtoProfile());
                cfg.AddProfile(new DtoToViewModelProfile());

                cfg.AddProfile(new DtoToEntityProfile());
                cfg.AddProfile(new EntityToDtoProfile());
            });
        }
    }
}