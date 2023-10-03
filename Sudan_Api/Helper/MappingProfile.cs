using AutoMapper;
using Sudan_Api.Dto;
using Sudan_Api.Models;

namespace Sudan_Api.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Sudan, SudanDto>();
            CreateMap<SudanCategory, SudanCategoryDto>();
            CreateMap<SudanDto, Sudan>();
            CreateMap<SudanCategoryDto, SudanCategory>();

        }
    }
}
