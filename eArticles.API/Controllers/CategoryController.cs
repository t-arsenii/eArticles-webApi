using eArticles.API.Data;
using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using eArticles.API.Services.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eArticles.API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CategoryController : ControllerBase
{
    ICategoryRepository _categoryRepo;
    public CategoryController(ICategoryRepository categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _categoryRepo.GetById(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryRepo.GetAll();
        List<CategoryDto> categoryDtos = new();
        foreach (var category in categories)
        {
            categoryDtos.Add(new CategoryDto(Id: category.Id, Title: category.Title));
        }
        if (!categoryDtos.Any())
        {
            return NotFound();
        }
        return Ok(categoryDtos);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateCategoryDto newCategory)
    {
        Category category = new();
        category.Title = newCategory.Title;
        var createdCategory = await _categoryRepo.Create(category);
        if (createdCategory == null)
        {
            return BadRequest();
        }
        return Ok(new CategoryDto(createdCategory.Id, createdCategory.Title));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryDto updateCategoryDto)
    {
        Category category = new Category() { Id = id, Title = updateCategoryDto.Title };
        var updateCategory = await _categoryRepo.Update(category);
        if (updateCategory == null)
        {
            return NotFound();
        }
        return Ok(new CategoryDto(updateCategory.Id, updateCategory.Title));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedCategory = await _categoryRepo.Delete(id);
        if (deletedCategory == null)
        {
            return NotFound();
        }
        return Ok(new CategoryDto(deletedCategory.Id, deletedCategory.Title));
    }
}

