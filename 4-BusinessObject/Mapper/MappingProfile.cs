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
            CreateMap<Category, RequestDTO.RequestDTO.CategoryListDTO>();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>();
            CreateMap<Category, CategoryDetailDTO>();

            // DesignElement Mapping
            CreateMap<DesignElement, DesignElementDTO>()
                .ForMember(dest => dest.DesignElementId, opt => opt.MapFrom(src => src.DesignElementId))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
                .ForMember(dest => dest.ColorArea, opt => opt.MapFrom(src => src.ColorArea))
                .ForMember(dest => dest.DesignAreaId, opt => opt.MapFrom(src => src.DesignAreaId))
                .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.DesignArea.AreaName))
                .ForMember(dest => dest.CustomizeProductId, opt => opt.MapFrom(src => src.CustomizeProductId))
                .ForMember(dest => dest.ShirtColor, opt => opt.MapFrom(src => src.CustomizeProduct.ShirtColor))
                .ForMember(dest => dest.FullImage, opt => opt.MapFrom(src => src.CustomizeProduct.FullImage))
                .ReverseMap();
            // Mapping DesignElementCreateDTO <-> DesignElement
            CreateMap<DesignElementCreateDTO, DesignElement>()
                .ForMember(dest => dest.Image, opt => opt.Ignore()) // Image sẽ xử lý riêng (upload file)
                .ForMember(dest => dest.DesignElementId, opt => opt.Ignore()) // ID sẽ được tạo tự động
                .ForMember(dest => dest.DesignArea, opt => opt.Ignore()) // Không map trực tiếp entity liên quan
                .ForMember(dest => dest.CustomizeProduct, opt => opt.Ignore());


            //Oder
            CreateMap<Order, OrderListDTO>().ReverseMap();
            CreateMap<OrderCreateDTO, Order>().ReverseMap();
            CreateMap<OrderUpdateDTO, Order>().ReverseMap();

            // OrderStage
            CreateMap<OrderStage, OrderStageListDTO>()
                  .ForMember(dest => dest.OrderStageName,
                             opt => opt.MapFrom(src => src.OrderStageName.ToString()))
                  .ReverseMap()
                  .ForMember(dest => dest.OrderStageName,
                             opt => opt.MapFrom(src => (OrderStageEnum)Enum.OrderStageEnum.Parse(typeof(OrderStageEnum), src.OrderStageName)));



            // Map từ OrderStageCreateDTO sang Entity
            CreateMap<OrderStageCreateDTO, OrderStage>().ReverseMap();

        }
    }
}
