using AutoMapper;
using LibraryManagementSystem.Dtos.Category;
using LibraryManagementSystem.Models.Category;

namespace LibraryManagementSystem.Mappings
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Categorys, CategoryDto>();
            CreateMap<CreateCategoryDto, Categorys>();
            CreateMap<UpdateCategoryDto, Categorys>();
        }
    }
}
