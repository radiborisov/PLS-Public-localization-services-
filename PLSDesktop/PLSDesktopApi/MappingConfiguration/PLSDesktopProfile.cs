using AutoMapper;
using PLSDesktopApi.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLSDesktopApi.MappingConfiguration
{
    public class PLSDesktopProfile : Profile
    {
        public PLSDesktopProfile()
        {
            CreateMap<UserDto, ChangeUsersRank>();
        }
    }
}
