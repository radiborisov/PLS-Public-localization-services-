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

            this.CreateMap<PutUserView, User>()
                .ForMember(x => x.IsSavior, y => y.MapFrom(x => x.IsSavior))
                .ForMember(x => x.PhoneNumber, opt => opt.Ignore());

        }
    }
}
