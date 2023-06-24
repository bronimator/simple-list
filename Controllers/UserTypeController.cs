using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserListTestApp.Data;
using UserListTestApp.Models;

namespace UserListTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper _mapper;

        public UserTypeController(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<List<UserTypeDto>>> GetUserTypes()
        {
            var userTypes = await appDbContext.UserTypes
                .ToListAsync();

            var usersTypesDto = userTypes.Select(_mapper.Map<UserTypeDto>);

            return Ok(usersTypesDto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserTypeDto>> GetUserType(int id)
        {
            var userType = await appDbContext.UserTypes
                .FirstOrDefaultAsync(e => e.Id == id);

            if (userType != null)
            {
                return Ok(_mapper.Map<UserTypeDto>(userType));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<List<UserTypeDto>>> AddUser(UserTypeDto userTypeDto)
        {
            if (userTypeDto != null)
            {
                var userType = _mapper.Map<UserType>(userTypeDto);

                appDbContext.UserTypes.Add(userType);

                await appDbContext.SaveChangesAsync();

                var userTypes = await appDbContext.UserTypes.ToListAsync();

                return Ok(userTypes);
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult<List<UserDto>>> DeleteUser(int id)
        {
            var userType = await appDbContext.UserTypes.FirstOrDefaultAsync(e => e.Id == id);

            if (userType != null)
            {
                appDbContext.UserTypes.Remove(userType);

                await appDbContext.SaveChangesAsync();

                var userTypes = await appDbContext.UserTypes
                    .ToListAsync();

                return Ok(userTypes.Select(_mapper.Map<UserDto>));
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<List<UserTypeDto>>> UpdateUser(UserTypeDto userTypeDto)
        {
            if (userTypeDto != null)
            {
                var userType = await appDbContext.UserTypes
                    .FirstOrDefaultAsync(e => e.Id == userTypeDto.Id);

                if (userType != null)
                {
                    userType.Name = userTypeDto.Name;
                    userType.Allow_edit = userTypeDto.Allow_edit;

                    await appDbContext.SaveChangesAsync();

                    var userTypes = await appDbContext.UserTypes.ToListAsync();

                    return Ok(userTypes);
                }

                return NotFound();
            }

            return BadRequest();
        }
    }
}
