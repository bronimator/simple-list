using AutoMapper;
using System;
using UserListTestApp.Models;

namespace UserListTestApp.Dtos
{
    public class AppMappingProfile: Profile
    {
        public AppMappingProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(x => x.Type, options => options.Ignore());

            CreateMap<UserJsonDto, User>()
                .ForMember(x => x.Type, options => options.Ignore());

            CreateMap<User, UserDto>()
                .ForMember(x => x.TypeId, options => options.MapFrom(x => x.Type.Id));

            CreateMap<UserTypeDto, UserType>().ReverseMap();
        }
    }
}
