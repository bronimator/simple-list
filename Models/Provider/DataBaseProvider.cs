using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using UserListTestApp.Data;

namespace UserListTestApp.Models.Provider
{
    public class DataBaseProvider : IDataProvider
    {
        private readonly AppDbContext _appDbContext;

        public DataBaseProvider(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var users = await _appDbContext.Users
               .Include(x => x.Type)
               .ToListAsync();

            return users;
        }

        public async Task<User> GetUserAsync(int id)
        {
            var user = await _appDbContext.Users
                .Include(x => x.Type)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }

            return user;
        }

        public async Task<List<User>> GetFilteredUsersAsync(UserFilteredDto filter)
        {
            var users = await _appDbContext.Users
                .Include(x => x.Type)
                .Where(x => string.IsNullOrWhiteSpace(filter.Name) || x.Name.Contains(filter.Name))
                .ToListAsync();

            users = users
                .Where(x => (!filter.DateFrom.HasValue || !filter.DateTo.HasValue) || x.Last_visit_date.HasValue && x.Last_visit_date >= filter.DateFrom.Value && x.Last_visit_date <= filter.DateTo.Value)
                .ToList();

            return users;
        }

        public async Task AddUserAsync(User user, int userDtoTypeId)
        {
            var userType = await _appDbContext.UserTypes.FirstOrDefaultAsync(x => x.Id == userDtoTypeId);

            if (userType != null)
            {
                user.Type = userType;
            }

            _appDbContext.Users.Add(user);

            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(int id)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(e => e.Id == id);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            _appDbContext.Users.Remove(user);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var user = await _appDbContext.Users
                .Include(x => x.Type)
                .FirstOrDefaultAsync(e => e.Id == userDto.Id);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            user.Name = userDto.Name;
            user.Login = userDto.Login;
            user.Password = userDto.Password;

            if (user.Type.Id != userDto.TypeId)
            {
                var userType = await _appDbContext.UserTypes.FirstOrDefaultAsync(x => x.Id == userDto.TypeId);

                if (userType != null)
                {
                    user.Type = userType;
                }
            }

            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<UserType>> GetUserTypesAsync()
        {
            var userTypes = await _appDbContext.UserTypes
               .ToListAsync();

            return userTypes;
        }
    }
}
