using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Exceptions;
using LiveLessons.BLL.Interfaces;
using LiveLessons.DAL.Entities;
using LiveLessons.DAL.Interfaces;

namespace LiveLessons.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public UserDto Get(int id)
        {
            var user = unitOfWork.Users.Get(id);
            var userDto = mapper.Map<UserDto>(user);

            return userDto;
        }

        public UserDto GetByProfileId(string profileId)
        {
            var user = unitOfWork.Users.Find(entity => entity.ProfileId.Equals(profileId)).FirstOrDefault();
            var userDto = mapper.Map<UserDto>(user);

            return userDto;
        }

        public IEnumerable<UserDto> GetAll()
        {
            var users = unitOfWork.Users.GetAll().ToList();
            var usersDto = mapper.Map<IEnumerable<UserDto>>(users);

            return usersDto;
        }

        public void Create(UserDto userDto)
        {
            var user = mapper.Map<User>(userDto);

            unitOfWork.Users.Create(user);
            unitOfWork.Save();
        }

        public void Edit(UserDto userDto)
        {
            var updatingUser = unitOfWork.Users.Get(userDto.Id);

            if (updatingUser == null)
            {
                throw new EntityNotFoundException($"There is no User with id { userDto.Id } in the database.", "User");
            }

            userDto.ProfileId = updatingUser.ProfileId;
            mapper.Map(userDto, updatingUser);

            unitOfWork.Users.Update(updatingUser);
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            unitOfWork.Users.Delete(id);
            unitOfWork.Save();
        }
    }
}