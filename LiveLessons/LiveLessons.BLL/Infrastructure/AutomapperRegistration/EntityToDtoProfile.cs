using AutoMapper;
using LiveLessons.BLL.DTO;
using LiveLessons.DAL.Entities;

namespace LiveLessons.BLL.Infrastructure.AutomapperRegistration
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {

			CreateMap<User, UserDto>();

			CreateMap<Course, CourseDto>();

			CreateMap<Message, MessageDto>();

			CreateMap<Appointment, AppointmentDto>();
        }
    }
}
