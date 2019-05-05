using AutoMapper;
using PLSDataBase.Models;
using PLSServerForDesktop.ViewModels.Locations;
using PLSServerForDesktop.ViewModels.Users;

namespace PLSServerForDesktop.MappingConfiguration
{
    public class PLSProfile : Profile
    {
        public PLSProfile()
        {
            this.CreateMap<User, CreateUserAllView>();

            this.CreateMap<Location, CreateLocationAllView>();

            this.CreateMap<PatchUserView, User>();
        }
    }
}
