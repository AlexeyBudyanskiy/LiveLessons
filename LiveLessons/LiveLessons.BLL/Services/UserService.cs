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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public UserDto Get(int id)
        {
            var user = _unitOfWork.Users.Get(id);
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public UserDto GetByProfileId(string profileId)
        {
            var user = _unitOfWork.Users.Find(entity => entity.ProfileId.Equals(profileId)).FirstOrDefault();
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public IEnumerable<UserDto> GetAll()
        {
            var users = _unitOfWork.Users.GetAll().ToList();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return usersDto;
        }

        public void Create(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            _unitOfWork.Users.Create(user);
            _unitOfWork.Save();
        }

        public void Edit(UserDto userDto)
        {
            var updatingUser = _unitOfWork.Users.Get(userDto.Id);

            if (updatingUser == null)
            {
                throw new EntityNotFoundException($"There is no User with id { userDto.Id } in the database.", "User");
            }

            _mapper.Map(userDto, updatingUser);

            _unitOfWork.Users.Update(updatingUser);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Users.Delete(id);
            _unitOfWork.Save();
        }
    }
}