using AutoMapper;
using PLSDataBase.Models;
using PLSMobileServer.ViewModels.User;
using PLSServer.ViewModels.Location;
using PLSServer.ViewModels.User;

namespace PLSServer.MappingConfiguration
{
    public class PLSProfile : Profile
    {
        public PLSProfile()
        {
            this.CreateMap<CreateInputUser, User>();

            this.CreateMap<User, CreateOutputUser>();

            this.CreateMap<CreateInputLocation, Location>();

            this.CreateMap<RegisterInputUser, User>();

            this.CreateMap<ChangeUserCondition, User>();
        }
    }
}
