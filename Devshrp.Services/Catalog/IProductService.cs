using Devsharp.Services.DTOs;

namespace Devsharp.Services.Catalog
{
    public interface IProductService
    {
        Task AddProductToCategory(ProductCategoryDTO productCategoryDTO);
        Task<bool> IsExistsProdcutAsync(int id);
        Task<ProductDTO> RegisterProductAsync(ProductDTO productDTO);
        Task RemoveProductAsync(int id);
        Task RemoveProductToCategory(ProductCategoryDTO productCategoryDTO);
        Task<IEnumerable<ProductListItem>> SearchAllProductsAsync();
        Task<ProductListItem> SearchPRoductByIdAsync(int id);
        Task<IEnumerable<ProductListItem>> SearchProductsAsync(ProductFilterDTO productFilterDTO);
        Task<IEnumerable<ProductListItem>> SearchUnAvailableProductAsync();
        Task UpdateProductAsync(ProductDTO productDTO);
        Task UpdateProductStockQuantityAsync(ProductStockQuantityDTO productStockQuantityDTO);
    }
}