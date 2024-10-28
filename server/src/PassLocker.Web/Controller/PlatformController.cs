using Microsoft.AspNetCore.Mvc;
using PassLocker.Application.Service.Interfaces;
using PassLocker.Domain.Entity;
using PassLocker.Web.Filter;
using PassLocker.Web.Request;
using PassLocker.Web.Response;

namespace PassLocker.Web.Controller;

[SessionVerifier]
[ApiController]
[Route("api/v1/platforms")]
public class PlatformController : ControllerBase
{
	private readonly ILogger<PlatformController> _logger;
	private readonly IPlatformService _service;

	public PlatformController(ILogger<PlatformController> logger, IPlatformService service)
	{
		_logger = logger;
		_service = service;
	}

	[HttpGet]
	[Route("{categoryId}")]
	public async Task<IActionResult> GetPlatformsByCategory(int categoryId)
	{
		try
		{
			var platforms = await _service.GetPlatformsByCategoryAsync(categoryId);
			_logger.LogInformation("Platforms found");
			return Ok(new ApiResponse("Platforms found", platforms));
		}
		catch (Exception ex)
		{
			_logger.LogError("Is {ex.Message}", ex.Message);
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}

	[HttpPost]
	public async Task<IActionResult> CreatePlatform([FromBody] PlatformRequest body)
	{
		try
		{
			await _service.SavePlatformAsync(
				new Platform
				{
					Name = body.Name,
					UrlImage = body.UrlImage,
					CategoryId = body.CategoryId
				}
			);
			_logger.LogInformation("Platform {body.Name} created", body.Name);
			return Ok(new ApiResponse("Platform created", true));
		}
		catch (Exception ex)
		{
			_logger.LogError("Is {ex.Message}", ex.Message);
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}

	[HttpPut]
	[Route("{id}")]
	public async Task<IActionResult> UpdatePlatform([FromBody] PlatformRequest body, int id)
	{
		try
		{
			await _service.UpdatePlatformAsync(
				new Platform
				{
					Id = id,
					Name = body.Name,
					UrlImage = body.UrlImage,
					CategoryId = body.CategoryId
				}
			);
			_logger.LogInformation("Platform with id {id} is updated", id);
			return Ok(new ApiResponse("Platform updated", true));
		}
		catch (Exception ex)
		{
			_logger.LogError("Is {ex.Message}", ex.Message);
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}

	[HttpDelete]
	[Route("{id}")]
	public async Task<IActionResult> DeletePlatform(int id)
	{
		try
		{
			await _service.DeletePlatformAsync(id);
			_logger.LogInformation("Platform with id {id} is removed", id);
			return Ok(new ApiResponse("Platform removed", true));
		}
		catch (Exception ex)
		{
			_logger.LogError("Is {ex.Message}", ex.Message);
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}
}