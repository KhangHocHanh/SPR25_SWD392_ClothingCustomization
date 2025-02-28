using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObject.Enum;
using BusinessObject.Model;
using static BusinessObject.RequestDTO.RequestDTO;
using static BusinessObject.ResponseDTO.ResponseDTO;

namespace BusinessObject.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginResponse>().ReverseMap();
            CreateMap<User, UserListDTO>()
           .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
           .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .IgnoreAllPropertiesWithAnInaccessibleSetter();

            // Product
            CreateMap<Product, ProductListDTO>();
            CreateMap<ProductCreateDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>();

            // Feedback
            CreateMap<FeedbackDTO, Feedback>().ReverseMap();
            
        }
    }
}
