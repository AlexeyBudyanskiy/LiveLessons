using System.Collections.Generic;
using LiveLessons.BLL.DTO;

namespace LiveLessons.BLL.Interfaces
{
    public interface IUserService
    {
        UserDto Get(int id);
        UserDto GetByProfileId(string profileId);
        IEnumerable<UserDto> GetAll();
        void Create(UserDto userDto);
        void Edit(UserDto userDto);
        void Delete(int id);
    }
}
