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
            CreateMap<Course, CourseDto>()
                .BeforeMap((course, dto) => dto.Coord = new Coord { X = course.CoordX, Y = course.CoordY });
            CreateMap<Message, MessageDto>();
            CreateMap<Appointment, AppointmentDto>();
        }
    }
}
