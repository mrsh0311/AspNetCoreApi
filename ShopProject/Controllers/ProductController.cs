using Devsharp.Services.Catalog;
using Devsharp.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productService.SearchAllProductsAsync());
        }
        [HttpGet("find/{id}")]
        public async Task<IActionResult> Find(int id)
        {
            var product = await _productService.SearchPRoductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpGet("Search")]
        public async Task<IActionResult> SearchProduct([FromQuery] ProductFilterDTO productFilterDTO)
        {
            return Ok(await _productService.SearchProductsAsync(productFilterDTO));
        }
        public async Task<IActionResult> RegisterAsync([FromForm] ProductDTO productDTO)
        {
            if (productDTO.ID != 0)
            {
                return BadRequest();
            }
            await _productService.RegisterProductAsync(productDTO);
            return CreatedAtAction("find", new { id = productDTO.ID }, productDTO);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] ProductDTO productDTO)
        {
            if (!await _productService.IsExistsProdcutAsync(productDTO.ID))
            {
                return NotFound();
            }
            await _productService.UpdateProductAsync(productDTO);
            return NoContent();
        }
        [HttpDelete("id")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            if (!await _productService.IsExistsProdcutAsync(id))
            {
                return NotFound();
            }
            await _productService.RemoveProductAsync(id);
            return Ok();
        }
        [HttpPut("UpdateProductStockQuantity")]
        public async Task<IActionResult> UpdateProductStockQuantityAsync([FromForm] ProductStockQuantityDTO productStockQuantityDTO)
        {

            if (!await _productService.IsExistsProdcutAsync(productStockQuantityDTO.ID))
            {
                return NotFound();
            }
            await _productService.UpdateProductStockQuantityAsync(productStockQuantityDTO);

            return NoContent();
        }
        public async Task<IActionResult> AddProductToCategoryAsync([FromForm] ProductCategoryDTO productCategoryDTO)
        {
            if (!await _productService.IsExistsProdcutAsync(productCategoryDTO.ProductID))
            {
                return NotFound(" Product Not  Found By Id :" + productCategoryDTO.ProductID);
            }

            if (!await _categoryService.IsExistsCategoryAsync(productCategoryDTO.CategoryID))
            {
                return NotFound(" Category Not  Found By Id :" + productCategoryDTO.CategoryID);
            }

            await _productService.AddProductToCategory(productCategoryDTO);
            return Ok();
        }
        [HttpDelete("RemoveProductFromCategory")]
        public async Task<IActionResult> RemoveProductFromCategoryAsync([FromForm] ProductCategoryDTO productCategoryDTO)
        {
            if (!await _productService.IsExistsProdcutAsync(productCategoryDTO.ProductID))
            {
                return NotFound(" Product Not  Found By Id :" + productCategoryDTO.ProductID);
            }

            if (!await _categoryService.IsExistsCategoryAsync(productCategoryDTO.CategoryID))
            {
                return NotFound(" Category Not  Found By Id :" + productCategoryDTO.CategoryID);
            }

            await _productService.RemoveProductToCategory(productCategoryDTO);
            return Ok();

        }
    }
}
