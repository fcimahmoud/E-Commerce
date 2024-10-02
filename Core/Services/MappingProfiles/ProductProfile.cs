

global using AutoMapper;
using Domain.Entities;
using Shared;

namespace Services.MappingProfiles
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResultDTO>()
                .ForMember(d => d.BrandName, options => options.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.TypeName, options => options.MapFrom(s => s.ProductType.Name));

            CreateMap<ProductBrand, BrandResultDTO>();
            CreateMap<ProductType, TypeResultDTO>();
        }
    }
}
