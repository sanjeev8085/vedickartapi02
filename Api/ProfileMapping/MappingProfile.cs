using AutoMapper;

using Entities.Models;
using VadicKart.Entity.Presentation.Dto.ContactMessage;
using VadicKart.Entity.Presentation.Dto.LogIn;

namespace vedickartApi.Api.ProfileMapping

{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {    // contact massagecs 
            CreateMap<ContactMassagecsCreationDto, ContactMassagecs>();
            //CreateMap<ContactMassagecsCreationDto, ContactMassagecs>();
            CreateMap<ContactMassagecs, ContactMassagecsDto>();

            CreateMap<Sign_up_, Sign_up_Dto>();
            CreateMap<Sign_up_Dto, Sign_up_>();
        }
    }
}
