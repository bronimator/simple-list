using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UserListTestApp.Data;

namespace UserListTestApp.Models.Provider
{
    public class FileProvider : IDataProvider
    {
        public async Task<List<User>> GetUsersAsync()
        {
            var userData = await File.ReadAllTextAsync("Users.json");
            var userTypeData = await File.ReadAllTextAsync("UserTypes.json");

            var usersDto = JsonConvert.DeserializeObject<List<UserJsonDto>>(userData);
            var userTypes = JsonConvert.DeserializeObject<List<UserType>>(userTypeData);

            if (usersDto == null)
            {
                throw new UserNotFoundException();
            }

            return usersDto.Select(x => MapUserJsonDtoToUser(x, userTypes)).ToList();
        }

        public async Task<User> GetUserAsync(int id)
        {
            var userData = await File.ReadAllTextAsync("Users.json");
            var userTypeData = await File.ReadAllTextAsync("UserTypes.json");

            var usersDto = JsonConvert.DeserializeObject<List<UserJsonDto>>(userData);
            var userTypes = JsonConvert.DeserializeObject<List<UserType>>(userTypeData);

            var user = new User();

            if (usersDto == null)
            {
                throw new UserNotFoundException();
            }

            var userDto = usersDto.FirstOrDefault(x => x.Id == id);

            if (userDto == null)
            {
                throw new UserNotFoundException();
            }

            var userType = userTypes?.FirstOrDefault(y => y.Id == userDto.Type_Id);

            if (userType == null)
            {
                userType = new UserType();
            }

            user = new User()
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Login = userDto.Login,
                Password = userDto.Password,
                Type = userType,
                Last_visit_date = userDto.Last_visit_date,
            };

            return user;
        }

        public async Task<List<User>> GetFilteredUsersAsync(UserFilteredDto filter)
        {
            var userData = await File.ReadAllTextAsync("Users.json");
            var userTypeData = await File.ReadAllTextAsync("UserTypes.json");

            var usersDto = JsonConvert.DeserializeObject<List<UserJsonDto>>(userData);
            var userTypes = JsonConvert.DeserializeObject<List<UserType>>(userTypeData);

            if (usersDto == null)
            {
                throw new UserNotFoundException();
            }

            usersDto = usersDto
                .Where(x => string.IsNullOrWhiteSpace(filter.Name) || x.Name.Contains(filter.Name))
                .Where(x => (!filter.DateFrom.HasValue || !filter.DateTo.HasValue) || x.Last_visit_date.HasValue && x.Last_visit_date >= filter.DateFrom.Value && x.Last_visit_date <= filter.DateTo.Value)
                .ToList();

            return usersDto.Select(x => MapUserJsonDtoToUser(x, userTypes)).ToList();
        }

        public async Task AddUserAsync(User user, int userDtoTypeId)
        {
            var userData = await File.ReadAllTextAsync("Users.json");

            var usersDto = JsonConvert.DeserializeObject<List<UserJsonDto>>(userData);

            if (usersDto == null)
            {
                throw new UserNotFoundException();
            }

            var newUser = CreateUserJsonDto(user, userDtoTypeId, usersDto);

            usersDto.Add(newUser);

            File.WriteAllText("Users.json", JsonConvert.SerializeObject(usersDto));
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var userData = await File.ReadAllTextAsync("Users.json");

            var usersDto = JsonConvert.DeserializeObject<List<UserJsonDto>>(userData);

            if (usersDto == null)
            {
                throw new UserNotFoundException();
            }

            var userJsonDto = usersDto.FirstOrDefault(x => x.Id == userDto.Id);

            if (userJsonDto == null)
            {
                throw new UserNotFoundException();
            }

            userJsonDto.Name = userDto.Name;
            userJsonDto.Login = userDto.Login;
            userJsonDto.Password = userDto.Password;
            userJsonDto.Type_Id = userDto.TypeId;

            File.WriteAllText("Users.json", JsonConvert.SerializeObject(usersDto));
        }

        public async Task DeleteUserAsync(int id)
        {
            var userData = await File.ReadAllTextAsync("Users.json");

            var usersDto = JsonConvert.DeserializeObject<List<UserJsonDto>>(userData);

            if (usersDto == null)
            {
                throw new UserNotFoundException();
            }

            usersDto = usersDto.Where(x => x.Id != id).ToList();

            File.WriteAllText("Users.json", JsonConvert.SerializeObject(usersDto));
        }

        private User MapUserJsonDtoToUser(UserJsonDto userJsonDto, List<UserType>? userTypes)
        {
            var userType = userTypes?.FirstOrDefault(y => y.Id == userJsonDto.Type_Id);

            if (userType == null)
            {
                userType = new UserType();
            }

            return new User()
            {
                Id = userJsonDto.Id,
                Name = userJsonDto.Name,
                Login = userJsonDto.Login,
                Password = userJsonDto.Password,
                Type = userType,
                Last_visit_date = userJsonDto.Last_visit_date,
            };
        }

        public async Task<List<UserType>> GetUserTypesAsync()
        {
            var userTypeData = await File.ReadAllTextAsync("UserTypes.json");

            var userTypes = JsonConvert.DeserializeObject<List<UserType>>(userTypeData);

            if (userTypeData == null || userTypes == null)
            {
                throw new UserNotFoundException();
            }

            return userTypes;
        }

        public UserJsonDto CreateUserJsonDto(User user, int typeId, List<UserJsonDto> userJsonDtos)
        {
            return new UserJsonDto()
            {
                Id = GetNewId(userJsonDtos),
                Name = user.Name,
                Login = user.Login,
                Password = user.Password,
                Type_Id = typeId,
                Last_visit_date = user.Last_visit_date,
            };
        }

        public int GetNewId(List<UserJsonDto> userJsonDtos)
        {
            var lastId = userJsonDtos.Max(x => x.Id);

            return lastId + 1;
        }
    }
}
