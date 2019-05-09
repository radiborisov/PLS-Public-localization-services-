using AutoMapper;
using PLSAuthenticationServer.Models.UserModels;
using PLSDataBase.Models;
using PLSMobileAuthanticationDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLSAuthenticationServer.MappingConfiguration
{
    public class PLSRegisterProfile : Profile
    {
        public PLSRegisterProfile()
        {
            CreateMap<RegisterUserDto, MobileAuthanticationQueue>();
            CreateMap<LoginUserDto, User>();
        }
    }
}
