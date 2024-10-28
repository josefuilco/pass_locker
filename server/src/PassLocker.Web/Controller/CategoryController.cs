using Microsoft.AspNetCore.Mvc;
using PassLocker.Application.Service.Interfaces;
using PassLocker.Domain.Entity;
using PassLocker.Web.Filter;
using PassLocker.Web.Request;
using PassLocker.Web.Response;

namespace PassLocker.Web.Controller;

[SessionVerifier]
[ApiController]
[Route("api/v1/categories")]
public class CategoryController : ControllerBase
{
	private readonly ILogger<CategoryController> _logger;
	private readonly ICategoryService _service;

	public CategoryController(ILogger<CategoryController> logger, ICategoryService service)
	{
		_logger = logger;
		_service = service;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllCategories()
	{
		try
		{
			var categories = await _service.GetAllCategoriesAsync();
			_logger.LogInformation("Categories found");
			return Ok(new ApiResponse("Categories found", categories));
		}
		catch (Exception ex)
		{
			_logger.LogError("Is {ex.Message}", ex.Message);
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}

	[HttpPost]
	public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest body)
	{
		try
		{
			await _service.SaveCategoryAsync(new Category { Name = body.Name, IsActive = true });
			_logger.LogInformation("Category is created");
			return Ok(new ApiResponse("Category is created", true));
		}
		catch (Exception ex)
		{
			_logger.LogError("Is {ex.Message}", ex.Message);
			return BadRequest(new ApiResponse(ex.Message, false));
		}
	}

	[HttpPut]
	[Route("{id}")]
	public async Task<IActionResult> UpdateCategory([FromBody] CategoryRequest body, int id)
	{
		try
		{
			await _service.UpdateCategoryAsync(new Category { Id = id, Name = body.Name });
			_logger.LogInformation("Category with id {id} is updated", id);
			return Ok(new ApiResponse("Category updated", true));
		}
		catch (Exception ex)
		{
			_logger.LogError("Is {ex.Message}", ex.Message);
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}

	[HttpDelete]
	[Route("{id}")]
	public async Task<IActionResult> DeleteCategory(int id)
	{
		try
		{
			await _service.DeleteCategoryAsync(id);
			_logger.LogInformation("Category with id {id} is removed", id);
			return Ok(new ApiResponse("Category removed", true));
		}
		catch (Exception ex)
		{
			_logger.LogError("Is {ex.Message}", ex.Message);
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}
}