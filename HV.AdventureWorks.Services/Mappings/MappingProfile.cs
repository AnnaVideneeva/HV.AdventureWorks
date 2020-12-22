using AutoMapper;
using HV.AdventureWorks.Data.Entities;
using HV.AdventureWorks.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace HV.AdventureWorks.Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductEntity>().ReverseMap();
            CreateMap<Document, DocumentEntity>()
                .ForMember(x => x.DocumentNode, opt => opt.MapFrom(r => HierarchyId.Parse(r.DocumentNode)))
                .ReverseMap()
                .ForMember(x => x.DocumentNode, opt => opt.MapFrom(r => r.DocumentNode.ToString()));
        }
    }
}
