using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using UserListTestApp.Data;
using UserListTestApp.Models;
using UserListTestApp.Models.Provider;

namespace UserListTestApp.Services
{
    public class ProviderService: IDataProvider
    {
        private readonly IDataProvider _dataBaseProvider;
        private readonly IDataProvider _fileProvider;
        public ProviderService(AppDbContext appDbContext)
        {
            _dataBaseProvider = new DataBaseProvider(appDbContext);
            _fileProvider = new FileProvider();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var source = DataSource.GetCurrentSourceType();

            switch (source) 
            {
                case Source.DataBase:
                    return await _dataBaseProvider.GetUsersAsync();
                case Source.File:
                    return await _fileProvider.GetUsersAsync();
                default:
                    throw new ArgumentException("Unknown data provider");
            }
        }

        public async Task<User> GetUserAsync(int id)
        {
            var source = DataSource.GetCurrentSourceType();

            switch (source)
            {
                case Source.DataBase:
                    return await _dataBaseProvider.GetUserAsync(id);
                case Source.File:
                    return await _fileProvider.GetUserAsync(id);
                default:
                    throw new ArgumentException("Unknown data provider");
            }
        }

        public async Task<List<User>> GetFilteredUsersAsync(UserFilteredDto filter)
        {
            var source = DataSource.GetCurrentSourceType();

            switch (source)
            {
                case Source.DataBase:
                    return await _dataBaseProvider.GetFilteredUsersAsync(filter);
                case Source.File:
                    return await _fileProvider.GetFilteredUsersAsync(filter);
                default:
                    throw new ArgumentException("Unknown data provider");
            }
        }

        public async Task AddUserAsync(User user, int userTypeId)
        {
            var source = DataSource.GetCurrentSourceType();

            switch (source)
            {
                case Source.DataBase:
                    await _dataBaseProvider.AddUserAsync(user, userTypeId);
                    break;
                case Source.File:
                    await _fileProvider.AddUserAsync(user, userTypeId);
                    break;
                default:
                    throw new ArgumentException("Unknown data provider");
            }
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var source = DataSource.GetCurrentSourceType();

            switch (source)
            {
                case Source.DataBase:
                    await _dataBaseProvider.UpdateUserAsync(userDto);
                    break;
                case Source.File:
                    await _fileProvider.UpdateUserAsync(userDto);
                    break;
                default:
                    throw new ArgumentException("Unknown data provider");
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            var source = DataSource.GetCurrentSourceType();

            switch (source)
            {
                case Source.DataBase:
                    await _dataBaseProvider.DeleteUserAsync(id);
                    break;
                case Source.File:
                    await _fileProvider.DeleteUserAsync(id);
                    break;
                default:
                    throw new ArgumentException("Unknown data provider");
            }
        }

        public async Task<List<UserType>> GetUserTypesAsync()
        {
            var source = DataSource.GetCurrentSourceType();

            switch (source)
            {
                case Source.DataBase:
                    return await _dataBaseProvider.GetUserTypesAsync();
                case Source.File:
                    return await _fileProvider.GetUserTypesAsync();
                default:
                    throw new ArgumentException("Unknown data provider");
            }
        }
    }
}
