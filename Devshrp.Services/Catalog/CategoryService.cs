﻿using Devsharp.Core.Domian;
using Devsharp.Core.Extensions;
using Devsharp.Data;
using Devsharp.Services.DTOs;
using Devsharp.Services.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsharp.Services.Catalog
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repositoryCategory = null;

        public CategoryService(IRepository<Category> repositoryCategory)
        {
            _repositoryCategory = repositoryCategory;
        }


        public async Task<IEnumerable<CategoryListItemDTO>> SearchCategoriesAsync()
        {
            
            var _list = await _repositoryCategory.TableNoTracking
              .Select(p => new CategoryListItemDTO
              {
                  CreateOn = p.CreateOn,
                  ID = p.ID,
                  Name = p.Name,
                  ParentId = p.ParentId,
                  ParentName = p.ParentCategory.Name,
                  UpdateOn = p.UpdateOn,
                  ChildCount = p.Children.Count,
                  ProductCount = p.ProductCategories.Count,
                  LocalCreateOn = p.CreateOn.ToPersian(),
                  LocalUpdateOn = p.UpdateOn.ToPersian()
              }).ToListAsync();


            return _list;
        }

        public async Task<CategoryListItemDTO> SearchCategoryByIdAsync(int id)
        {
            var category = await _repositoryCategory.GetByIdAsync(id);

            return category.TODTO<CategoryListItemDTO>();
        }

        public async Task<CategoryDTO> RegisterCategoryAsync(CategoryDTO categoryDto)
        {
            var category = categoryDto.ToEntity<Category>();
            await _repositoryCategory.InsertAsync(category);
            categoryDto.ID = category.ID;

            return categoryDto;
        }
        public async Task<bool> IsExistsCategoryAsync(int id)
        {
            var category = await _repositoryCategory.GetByIdAsNoTrackingAsync(id);
            if (category == null)
                return false;

            return true;
        }


        public async Task RemoveCategoryAsync(int id)
        {
            var category = _repositoryCategory.GetById(id);
            await _repositoryCategory.DeleteAsync(category);
        }

        public async Task UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            var category = _repositoryCategory.GetById(categoryDTO.ID);
            category.Name = categoryDTO.Name;
            category.ParentId = categoryDTO.ParentId;

            await _repositoryCategory.UpdateAsync(category);
        }
    }
}
