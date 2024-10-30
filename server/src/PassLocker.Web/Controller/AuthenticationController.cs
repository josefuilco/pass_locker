using Microsoft.AspNetCore.Mvc;
using PassLocker.Application.Service.Interfaces;
using PassLocker.Web.Configuration;
using PassLocker.Web.Request;
using PassLocker.Web.Response;
using PassLocker.Web.Filter;

namespace PassLocker.Web.Controller;

[ApiController]
[Route("api/v1/auth")]
public class AuthenticationController : ControllerBase
{
	private readonly ILogger<AuthenticationController> _logger;
	private readonly CookieConfiguration _cookieConfig;
	private readonly IOwnerService _service;

	public AuthenticationController(
		ILogger<AuthenticationController> logger,
		CookieConfiguration cookieConfig,
		IOwnerService service
	)
	{
		_logger = logger;
		_cookieConfig = cookieConfig;
		_service = service;
	}

	[HttpPost]
	[Route("login")]
	public async Task<IActionResult> Login([FromBody] OwnerRequest body)
	{
		var response = false;
		var options = _cookieConfig.GetAccessOptions();
		try
		{
			response = await _service.AuthenticateOwnerAsync(body.Password);
			if (!response)
			{
				var isOwnerBanned = _cookieConfig.GetCookieAccessIntent() == 3;
				if (isOwnerBanned)
				{
					options = _cookieConfig.GetBanOptions();
					Response.Cookies.Append("Access", "Banned", options);
					return StatusCode(401, new ApiResponse("Banned", response));
				}
				return StatusCode(401, new ApiResponse("Failed", response));
			}
			Response.Cookies.Append("Access", "Success", options);
			return Ok(new ApiResponse("Success", response));
		}
		catch (Exception ex)
		{
			return StatusCode(500, new ApiResponse(ex.Message, response));
		}
	}

	[HttpGet]
	[Route("ban")]
	public IActionResult Ban()
	{
		try
		{
			var options = _cookieConfig.GetBanOptions();
			Response.Cookies.Append("Access", "Banned", options);
			return Ok(new ApiResponse("Banned for 24h", true));
		}
		catch (Exception ex)
		{
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}

	[SessionVerifier]
	[HttpGet]
	[Route("logout")]
	public IActionResult Logout()
	{
		try
		{
			Response.Cookies.Delete("Access");
			return Ok(new ApiResponse("Logout", true));
		}
		catch (Exception ex)
		{
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}

	[SessionVerifier]
	[HttpPut]
	public async Task<IActionResult> UpdateMasterPassword([FromBody] OwnerRequest body)
	{
		try
		{
			await _service.SetupMasterPasswordAsync(body.Password);
			return Ok(new ApiResponse("Master password updated", true));
		}
		catch (Exception ex)
		{
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}
}