using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UserListTestApp.Data;
using UserListTestApp.Dtos;
using UserListTestApp.Models;
using UserListTestApp.Models.Provider;
using UserListTestApp.Services;

namespace UserListTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper _mapper;
        private readonly ProviderService _dataProvider;

        public UserController(AppDbContext appDbContext, IMapper mapper, ProviderService dataProvider)
        {
            this.appDbContext = appDbContext;
            _mapper = mapper;
            _dataProvider = dataProvider;
        }

        [HttpGet()]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            try
            {
                var users = await _dataProvider.GetUsersAsync();

                var usersDto = users.Select(_mapper.Map<UserDto>);

                return Ok(usersDto);
            }
            catch (ArgumentException ex)
            {
                // TODO Handle exception
                return NotFound();
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            try
            {
                var user = await _dataProvider.GetUserAsync(id);

                return Ok(_mapper.Map<UserDto>(user));
            }
            catch (ArgumentException ex)
            {
                // TODO Handle exception
                return NotFound();
            }
            catch (UserNotFoundException ex)
            {
                // TODO Handle exception
                return NotFound();
            }
        }

        [HttpPost]
        [Route("filter")]
        public async Task<ActionResult<List<UserDto>>> GetFilteredUsers(UserFilteredDto filter)
        {
            try
            {
                // Требование ТЗ - пятисекундная задержка
                Thread.Sleep(5000);

                var users = await _dataProvider.GetFilteredUsersAsync(filter);

                var usersDto = users
                    .Where(x => filter.TypeId == null || x.Type.Id == filter.TypeId)
                    .Select(_mapper.Map<UserDto>);

                return Ok(usersDto);
            }
            catch (ArgumentException ex)
            {
                // TODO Handle exception
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<UserDto>>> AddUser(UserDto userDto)
        {
            try
            {
                if (userDto == null)
                {
                    throw new ArgumentNullException(nameof(userDto));
                }

                var user = _mapper.Map<User>(userDto);

                await _dataProvider.AddUserAsync(user, userDto.TypeId);

                var users = await _dataProvider.GetUsersAsync();

                return Ok(users.Select(_mapper.Map<UserDto>));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest();
            }
            catch (ArgumentException ex)
            {
                // TODO Handle exception
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<List<UserDto>>> DeleteUser(int id)
        {
            try
            {
                await _dataProvider.DeleteUserAsync(id);

                var users = await _dataProvider.GetUsersAsync();

                return Ok(users.Select(_mapper.Map<UserDto>));
            }
            catch (UserNotFoundException ex)
            {
                // TODO Handle exception
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                // TODO Handle exception
                return NotFound();
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<UserDto>>> UpdateUser(UserDto userDto)
        {
            try
            {
                if (userDto == null)
                {
                    throw new ArgumentNullException(nameof(userDto));
                }

                await _dataProvider.UpdateUserAsync(userDto);

                var users = await _dataProvider.GetUsersAsync();

                return Ok(users.Select(_mapper.Map<UserDto>));
            }
            catch (UserNotFoundException ex)
            {
                // TODO Handle exception
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                // TODO Handle exception
                return NotFound();
            }
        }

        [HttpPut]
        [Route("source")]
        public ActionResult UpdateSourceType(DataSourceDto source)
        {
            DataSource.SetSourceType(source.SourceType);

            return Ok();
        }

        [HttpGet]
        [Route("source")]
        public ActionResult<int> GetSourceType()
        {
            return Ok(DataSource.GetCurrentSourceTypeInt());
        }
    }
}
