using AutoMapper;
using LiveLessons.BLL.DTO;
using LiveLessons.WEB.ViewModels;

namespace LiveLessons.WEB.AutomapperRegistrations
{
    public class DtoToViewModelProfile : Profile
    {
        public DtoToViewModelProfile()
        {
            CreateMap<ExampleDto, ExampleViewModel>();

			CreateMap<UserDto, UserViewModel>();

			CreateMap<CourseDto, CourseViewModel>();

			CreateMap<MessageDto, MessageViewModel>();

			CreateMap<AppointmentDto, AppointmentViewModel>();

        }
    }
}