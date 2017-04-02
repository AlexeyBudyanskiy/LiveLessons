using AutoMapper;
using LiveLessons.BLL.DTO;
using LiveLessons.WEB.ViewModels;

namespace LiveLessons.WEB.AutomapperRegistrations
{
    public class ViewModelToDtoProfile : Profile
    {
        public ViewModelToDtoProfile()
        {
            CreateMap<ExampleViewModel, ExampleDto>();

			CreateMap<UserViewModel, UserDto>();

			CreateMap<CourseViewModel, CourseDto>();

			CreateMap<MessageViewModel, MessageDto>();

			CreateMap<AppointmentViewModel, AppointmentDto>();

        }
    }
}