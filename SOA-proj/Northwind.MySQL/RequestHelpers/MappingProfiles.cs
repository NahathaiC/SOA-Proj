using AutoMapper;
using Northwind.Models;

namespace Northwind.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier));
            CreateMap<Category, CategoryDto>();
            CreateMap<Supplier, SupplierDto>();

            CreateMap<ProductDto, Product>()
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore()); 

            CreateMap<UpdateProductDto, Product>();

            CreateMap<CategoryDto, Category>();
            CreateMap<SupplierDto, Supplier>();
        }
    }
}

