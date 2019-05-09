using AutoMapper;
using PLSRegisterDB.Models;
using PLSRegistingServer.Models.UsersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLSRegistingServer.MappingConfiguration
{
    public class PLSRegisterProfile : Profile
    {
        public PLSRegisterProfile()
        {
            CreateMap<RegisterUserDto, MobileUserRegisterQueue>();
        }
    }
}
