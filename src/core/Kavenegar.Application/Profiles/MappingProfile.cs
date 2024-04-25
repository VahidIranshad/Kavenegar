using AutoMapper;
using Kavenegar.Application.Dto.Entity.BlogDtos;
using Kavenegar.Application.Dto.Entity.User;
using Kavenegar.Domain.Entity;

namespace Kavenegar.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Blog


            CreateMap<BlogDto, BLog>();
            CreateMap<BLog, BlogDto>()
                .ForMember(p => p.AuthorName, o => o.MapFrom(s => s.Author.Name));
            CreateMap<BlogCrudDto, BLog>().ReverseMap();
            #endregion

            #region User

            CreateMap<UserDto, User>().ReverseMap();
            #endregion
        }
    }
}
