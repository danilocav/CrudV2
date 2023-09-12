using AutoMapper;
using CrudV2.Business.DTOs;
using CrudV2.Business.Interfaces;
using CrudV2.Core.Entities;
using CrudV2.Core.Interfaces;
using CrudV2.Data;

namespace CrudV2.WebApi
{
    public class UserUseCases : IUserUseCases
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserUseCases(IUserRepository userRepository, IMapper mapper, ApplicationDbContext context)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _context = context;
        }
        public async Task<int> AddUserAsync(UserDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                Password = userDto.Password,
                Email = userDto.Email
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return userDtos;
        }
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        public async Task<bool> UpdateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var success = await _userRepository.UpdateUserAsync(user);
            return success;
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            var success = await _userRepository.DeleteUserAsync(id);
            return success;
        }
    }
}