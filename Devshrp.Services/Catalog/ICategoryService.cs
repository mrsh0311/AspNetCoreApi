using System.Collections.Generic;
using System.Threading.Tasks;
using Devsharp.Services.DTOs;

namespace Devsharp.Services.Catalog
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryListItemDTO>> SearchCategoriesAsync();
        Task<CategoryListItemDTO> SearchCategoryByIdAsync(int id);
        Task<CategoryDTO> RegisterCategoryAsync(CategoryDTO categoryDTO);
        Task<bool> IsExistsCategoryAsync(int id);
        Task RemoveCategoryAsync(int id);
        Task UpdateCategoryAsync(CategoryDTO categoryDTO);
    }
}