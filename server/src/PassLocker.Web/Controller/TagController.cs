using Microsoft.AspNetCore.Mvc;
using PassLocker.Application.Service.Interfaces;
using PassLocker.Domain.Entity;
using PassLocker.Web.Filter;
using PassLocker.Web.Request;
using PassLocker.Web.Response;

namespace PassLocker.Web.Controller;

[SessionVerifier]
[ApiController]
[Route("api/v1/tags")]
public class TagController : ControllerBase
{
	private readonly ILogger<TagController> _logger;
	private readonly ITagService _service;

	public TagController(ILogger<TagController> logger, ITagService service)
	{
		_logger = logger;
		_service = service;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllTags()
	{
		try
		{
			var tags = await _service.GetAllTagsAsync();
			_logger.LogInformation("Tags found");
			return Ok(new ApiResponse("Tags found", tags));
		}
		catch (Exception ex)
		{
			_logger.LogError("Is {ex.Message}", ex.Message);
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}

	[HttpPost]
	public async Task<IActionResult> CreateTag([FromBody] TagRequest body)
	{
		try
		{
			await _service.SaveTagAsync(
				new Tag
				{
					Name = body.Name,
					Description = body.Description,
					IsActive = true
				}
			);
			_logger.LogInformation("Tag created: {body.Name}", body.Name);
			return Ok(new ApiResponse("Tag created", true));
		}
		catch (Exception ex)
		{
			_logger.LogError("Is {ex.Message}", ex.Message);
			return BadRequest(new ApiResponse(ex.Message, false));
		}
	}

	[HttpPut]
	[Route("{id}")]
	public async Task<IActionResult> UpdateTag([FromBody] TagRequest body, int id)
	{
		try
		{
			await _service.UpdateTagAsync(
				new Tag
				{
					Id = id,
					Name = body.Name,
					Description = body.Description
				}
			);
			_logger.LogInformation("Tag with id {id} updated", id);
			return Ok(new ApiResponse("Tag updated", true));
		}
		catch (Exception ex)
		{
			_logger.LogError("Is {ex.Message}", ex.Message);
			return BadRequest(new ApiResponse(ex.Message, false));
		}
	}

	[HttpDelete]
	[Route("{id}")]
	public async Task<IActionResult> DeleteTag(int id)
	{
		try
		{
			await _service.DeleteTagAsync(id);
			_logger.LogInformation("Tag with id {id} removed", id);
			return Ok(new ApiResponse("Tag removed", true));
		}
		catch (Exception ex)
		{
			_logger.LogError("Is {ex.Message}", ex.Message);
			return BadRequest(new ApiResponse(ex.Message, false));
		}
	}
}