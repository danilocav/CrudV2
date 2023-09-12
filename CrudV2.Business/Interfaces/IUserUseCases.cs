using CrudV2.Business.DTOs;

namespace CrudV2.Business.Interfaces
{
    public interface IUserUseCases
    {
        Task<int> AddUserAsync(UserDto userDto);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(UserDto userDto);
        Task<bool> DeleteUserAsync(int id);
    }
}