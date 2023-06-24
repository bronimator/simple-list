using Microsoft.AspNetCore.Mvc;

namespace UserListTestApp.Models.Provider
{
    public interface IDataProvider
    {
        Task<List<User>> GetUsersAsync();

        Task<User> GetUserAsync(int id);

        Task<List<User>> GetFilteredUsersAsync(UserFilteredDto filter);

        Task AddUserAsync(User user, int userDtoTypeId);

        Task UpdateUserAsync(UserDto userDto);

        Task DeleteUserAsync(int id);

        Task<List<UserType>> GetUserTypesAsync();
    }
}
