using AutoMapper;
using HV.AdventureWorks.Data.Entities;
using HV.AdventureWorks.Services.Models;

namespace HV.AdventureWorks.Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductEntity>().ReverseMap();
        }
    }
}
