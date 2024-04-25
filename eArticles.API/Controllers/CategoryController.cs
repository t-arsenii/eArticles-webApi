using eArticles.API.Contracts.Category;
using eArticles.API.Data;
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
        return Ok(new CategoryResponse(Id: getCategoryResult.Value.Id, Title: getCategoryResult.Value.Title));
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
        List<CategoryResponse> categoryDtos = new();
        foreach (var category in getCategoriesResult.Value)
        {
            categoryDtos.Add(new CategoryResponse(Id: category.Id, Title: category.Title));
        }
        return Ok(categoryDtos);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateCategoryRequest createCategoryDto)
    {
        Category category = new();
        category.Title = createCategoryDto.Title;
        var createdCategory = await _categoryService.Create(category);
        if (createdCategory.IsError)
        {
            return BadRequest(createdCategory.IsError);
        }
        var categoryResponse = new CategoryResponse(createdCategory.Value.Id, createdCategory.Value.Title);
        return CreatedAtAction(actionName: (nameof(GetById)), routeValues: new { id = categoryResponse.Id }, value: categoryResponse);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequest updateCategoryDto)
    {
        Category category = new Category() { Id = id, Title = updateCategoryDto.Title };
        var updateCategoryResult = await _categoryService.Update(category);
        if (updateCategoryResult.IsError)
        {
            return NotFound(updateCategoryResult.FirstError.Description);
        }
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteCategoryResult = await _categoryService.Delete(id);
        if (deleteCategoryResult.IsError)
        {
            return NotFound(deleteCategoryResult.FirstError.Description);
        }
        return NoContent();
    }
}

