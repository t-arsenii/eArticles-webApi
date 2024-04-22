using eArticles.API.Data;
using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using eArticles.API.Persistance;
using eArticles.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eArticles.API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = "Admin")]
public class CategoryController : ControllerBase
{
    ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        var getCategoryResult = await _categoryService.GetById(id);
        if (getCategoryResult.IsError)
        {
            return NotFound(getCategoryResult.FirstError.Description);
        }
        return Ok(new CategoryDto(Id: getCategoryResult.Value.Id, Title: getCategoryResult.Value.Title));
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var getCategoriesResult = await _categoryService.GetAll();
        if (getCategoriesResult.IsError)
        {
            return NotFound(getCategoriesResult.FirstError.Description);
        }
        List<CategoryDto> categoryDtos = new();
        foreach (var category in getCategoriesResult.Value)
        {
            categoryDtos.Add(new CategoryDto(Id: category.Id, Title: category.Title));
        }
        return Ok(categoryDtos);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateCategoryDto createCategoryDto)
    {
        Category category = new();
        category.Title = createCategoryDto.Title;
        var createdCategory = await _categoryService.Create(category);
        if (createdCategory.IsError)
        {
            return BadRequest(createdCategory.IsError);
        }
        return Ok(new CategoryDto(createdCategory.Value.Id, createdCategory.Value.Title));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryDto updateCategoryDto)
    {
        Category category = new Category() { Id = id, Title = updateCategoryDto.Title };
        var updateCategoryResult = await _categoryService.Update(category);
        if (updateCategoryResult.IsError)
        {
            return NotFound(updateCategoryResult.FirstError.Description);
        }
        return Ok(new CategoryDto(updateCategoryResult.Value.Id, updateCategoryResult.Value.Title));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteCategoryResult = await _categoryService.Delete(id);
        if (deleteCategoryResult.IsError)
        {
            return NotFound(deleteCategoryResult.FirstError.Description);
        }
        return Ok(new CategoryDto(deleteCategoryResult.Value.Id, deleteCategoryResult.Value.Title));
    }
}

