using Devsharp.Services.Catalog;
using Devsharp.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _categoryService.SearchCategoriesAsync());
        }
        [HttpGet("find/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoryDto = await _categoryService.SearchCategoryByIdAsync(id);
            if (categoryDto == null)
            {
                return NotFound();
            }
            return Ok(categoryDto);
        }
        [HttpPost]
        public async Task<IActionResult> RegisterCategory([FromForm]CategoryDTO categoryDTO)
        {
            if (categoryDTO.ID!=0)
            {
                return BadRequest();
            }
            await _categoryService.RegisterCategoryAsync(categoryDTO);
            return CreatedAtAction("find",new {id=categoryDTO.ID},categoryDTO);
        }
        [HttpPut]
        public async Task<IActionResult> UpdaterCategory([FromForm] CategoryDTO categoryDTO)
        {
            if (!await _categoryService.IsExistsCategoryAsync(categoryDTO.ID))
            {
                return NotFound();
            }
            await _categoryService.UpdateCategoryAsync(categoryDTO);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            if (!await _categoryService.IsExistsCategoryAsync(id))
            {
                return NotFound();
            }
            await _categoryService.RemoveCategoryAsync(id);
            return Ok();
        }

    }
}
